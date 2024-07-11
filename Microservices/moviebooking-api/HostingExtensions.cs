using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation;
using MediatR;
using Messaging.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using moviebooking_api.Application.Behaviors;
using moviebooking_api.Application.EventHandlers;
using moviebooking_api.Application.Events;
using moviebooking_api.AutoMapper;
using moviebooking_api.Model;
using moviebooking_api.Options;
using moviebooking_api.Repositories;
using RabbitMQ.Infra;
using Serilog;

namespace moviebooking_api
{
    [ExcludeFromCodeCoverage]
    internal static class HostingExtensions
    {

        private const string SwaggerTitle = "Movie Service";
        private const string Version = "1";
        private const string Description = "Movie Service";
        private const string Name = "MovieServiceAPISpecification";

        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            ConfigureIOption(services, configuration);
            ConfigureScope(services, configuration);
            ConfigureBus(services, configuration);
            ConfigureController(services);

            var eventBus = builder.Services.BuildServiceProvider().GetRequiredService<IEventBus>();
            eventBus.Subscribe<AddNewEmplyeeEvent, AddNewEmployeeHandler>();
            eventBus.Subscribe<AddNewEmplyeeEvent, AddNewDummyEmployeeHandler>();

            services.AddApiVersioning(setup =>
            {
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.ReportApiVersions = true;
            })
            .ConfigureSwagger(SwaggerTitle, Version, Description, Name);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["Authentication:Authority"];
                    options.Audience = configuration["Authentication:Audience"];
                    options.TokenValidationParameters.ValidTypes = new[] { configuration["Authentication:ValidTypes"] };
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromSeconds(60);
                    options.TokenValidationParameters.ValidateLifetime = true;
                    options.RequireHttpsMetadata = false; // everything uses http in docker
                });

            return builder.Build();
        }

        private static void ConfigureBus(IServiceCollection services, IConfiguration configuration)
        {
            // if env is local 
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(scopeFactory);
            });


            services.AddTransient<AddNewEmployeeHandler>();
            services.AddTransient<AddNewDummyEmployeeHandler>();
            services.AddTransient<IEventHandler<AddNewEmplyeeEvent>, AddNewEmployeeHandler>();
            services.AddTransient<IEventHandler<AddNewEmplyeeEvent>, AddNewDummyEmployeeHandler>();


        }

        private static void ConfigureIOption(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbOptions.Position));
            services.AddHttpContextAccessor();
        }

        private static void ConfigureScope(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICinemaRepository, CinemaRepository>();
            services.AddSingleton<DbContext>();

            //registry?.Add("MongoDbCircuitBreakerPolicy",
            //    failurePolicies?.MongoDbCircuitBreakerPolicy);
            //services.AddSingleton<MongoDbHealthCheck>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddMediatR(typeof(ValidatorBehavior<,>).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));


            //services.AddSingleton<BackgroundServiceHealthCheck>();
            //services.AddHealthChecks()
            //    .AddCheck<MongoDbHealthCheck>("mongodb", tags: new[] { "ready" });
        }

        private static void ConfigureController(IServiceCollection services)
        {
            services.AddControllers() // Added for functional tests
                .AddApplicationPart(typeof(Program).Assembly)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, true));
                });
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSerilogRequestLogging();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                //{
                //    Predicate = check => check.Tags.Contains("ready"),
                //    ResponseWriter = HealthCheckResponseWriter.WriteResponse
                //});
                //endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
                //{
                //    Predicate = _ => false
                //});
            });

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/MovieServiceAPISpecification/swagger.json",
                    "MovieService");
                setupAction.RoutePrefix = "";
                setupAction.DocumentTitle = "Movie API documentation";
            });

            return app;
        }

        private static IServiceCollection ConfigureSwagger(this IServiceCollection services,
            string swaggerTitle, string version, string description, string name)
        {

            var assemblyDirectory = Path.GetDirectoryName(services.GetType().Assembly.Location);
            var projectFolder = new DirectoryInfo(assemblyDirectory).FullName;

            services.AddSwaggerGen(setupAction =>
            {
                const string license = "MIT License";
                const string contactUrl = "https://www.avalara.com";
                const string licenseUrl = "https://opensource.org/licenses/MIT";
                const string contactEmail = "user@avalara.com";
                const string contactName = "User";

                setupAction.SwaggerDoc(name,
                    new OpenApiInfo
                    {
                        Title = swaggerTitle,
                        Version = version,
                        Description = description,
                        Contact =
                            new OpenApiContact
                            {
                                Email = contactEmail,
                                Name = contactName,
                                Url = new Uri(contactUrl)
                            },
                        License = new OpenApiLicense
                        {
                            Name = license,
                            Url = new Uri(licenseUrl)
                        }
                    });
                var xmlFiles = Directory
                    .GetFiles(projectFolder, "*.xml", SearchOption.TopDirectoryOnly)
                    .ToList();
                xmlFiles.ForEach(xmlFile => setupAction.IncludeXmlComments(xmlFile));
                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                                { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
            });

            return services;

        }
    }
}
