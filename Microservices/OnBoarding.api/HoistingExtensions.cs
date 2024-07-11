using FluentValidation;
using MediatR;
using Messaging.Core;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnBoarding.api.Application.Behaviors;
using OnBoarding.api.Filter;
using OnBoarding.api.HeathChecks;
using OnBoarding.api.Helper.Clients;
using OnBoarding.api.Helper.HostedServices;
using OnBoarding.api.Mapper;
using OnBoarding.api.Middelware;
using OnBoarding.api.Models;
using OnBoarding.api.Options;
using OnBoarding.api.Repositories;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using RabbitMQ.Infra;
using Serilog;
using System.Reflection;
using static System.Net.WebRequestMethods;

namespace OnBoarding.api
{
    internal static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            //var connectionString = builder.Configuration.GetSection("ConnectionStrings");
            //Console.WriteLine(connectionString.GetValue<string>("DefaultConnection"));

            //Console.WriteLine(builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));

           
            builder.Services.Configure<DbOptions>(builder.Configuration.GetSection("ConnectionStrings"));
            var dbOptions = builder.Configuration.GetSection("ConnectionStrings").Get<DbOptions>();

            Log.Information("***** Connection string " + dbOptions.DefaultConnection);
            // 1. Create Db Context
            // 2. Create Migration scripts
            // 3. Run Migration scripts

            builder.Services.AddDbContext<OnboardingDBContext>(options =>
            {
                options.UseMySql(dbOptions.DefaultConnection, ServerVersion.AutoDetect(dbOptions.DefaultConnection));
            });

            Log.Information("***** Set the conext Done");

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //builder.Services.AddMediatR(typeof(HostingExtensions));
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
    
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddHttpClient<IMovieApiClient, MovieApiClient>(opt =>
            {
                opt.BaseAddress = new Uri("https://localhost:7132");
            });

            // if env is local 
            builder.Services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(scopeFactory);
            });

            builder.Services.AddScoped<IIncrementService, IncrementService>();
            builder.Services.AddHostedService<ExampleHostedService>();

            builder.Services.AddScoped<EmployeeFilter>();


            var registry = builder.Services.AddPolicyRegistry();
            registry.Add("httpretrypolicy", GetRetryPolicy().RetryPolicy);
            registry.Add("sqsretrypolicy", GetRetryPolicy().SQSPolicy);

            registry.Add("MysqlCircuitBreakerPolicy", GetCircuitBrekerRetryPolicy());

            //builder.Services.AddHealthChecks()
            //    .AddCheck<MySQLHealthCheck>("mysql", null, new[] { "ready" });
                //.AddCheck<MySQSHealthCheck>("sqs", null, new[] { "ready" });

            ConfigureSwagger(builder);

            builder.Services.AddCors(c =>
            {
                c.AddDefaultPolicy(options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
            });

            return builder.Build();
        }


        private static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(setupAction =>
            {
                //setupAction.SwaggerDoc("EmployeeOnBloading",
                //    new Microsoft.OpenApi.Models.OpenApiInfo
                //    {
                //        Title = "OnBoarding API",
                //        Description = "blabla",
                //        Contact = new Microsoft.OpenApi.Models.OpenApiContact { Email = "frank@gmail.com" },
                //        License = new Microsoft.OpenApi.Models.OpenApiLicense { Name = "OnBoadLic" }
                //    });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                setupAction.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            });
        }

        private static (AsyncRetryPolicy RetryPolicy, AsyncRetryPolicy SQSPolicy) GetRetryPolicy()
        {
            var retryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(3, provider =>
            {
               return TimeSpan.FromSeconds(5);
            });

            var sqsPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(2, provider =>
            {
                return TimeSpan.FromSeconds(5);
            });

            return (retryPolicy, sqsPolicy);
        }

        private static AsyncCircuitBreakerPolicy GetCircuitBrekerRetryPolicy()
        {
            var retryPolicy = Policy.Handle<Exception>().CircuitBreakerAsync(1, TimeSpan.FromSeconds(5), (_, _) =>
            {
                Log.Logger.Error("Circuit Broken");
            },
            () => Log.Logger.Information("Circuit is reset"));

            return retryPolicy;
        }

        public static WebApplication Configure(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<OnboardingDBContext>();
            Log.Information("***** before migration");
            dbContext.Database.Migrate();
            Log.Information("***** after migration");

            app.UseSerilogRequestLogging();

            //
            app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseMiddleware<PerformanceHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseCors();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
            //    {
            //        Predicate = check => check.Tags.Contains("ready"),
            //        ResponseWriter = WriteResponse
            //    });
            //});

            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();

            return app;
        }

        public static Task WriteResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json";
            var json = new JObject(new JProperty("status", result.Status.ToString()),
                new JProperty("results", new JObject(result.Entries.Select(pair =>
                    new JProperty(pair.Key, new JObject(
                        new JProperty("status", pair.Value.Status.ToString()),
                        new JProperty("description", pair.Value.Description),
                        new JProperty("data"
                         , new JObject(pair.Value.Data.Select(p =>
                                new JProperty(p.Key, p.Value))))))))));

            return context.Response.WriteAsync(json.ToString(Formatting.Indented));
        }
    }
}
