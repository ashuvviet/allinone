using OnBoarding.api;
using OnBoarding.api.Models;
using Serilog;

Log.Logger = new LogConfiguration().CreateLogger();


try
{

    var builder = WebApplication.CreateBuilder(args);

    //builder.Host
    //      .ConfigureAppConfiguration(delegate (HostBuilderContext ctx, IConfigurationBuilder configuration)
    //      {
    //          configuration.AddEnvironmentVariables();
    //      });

    builder.Host.UseSerilog();

    // Add services to the container.
    var app = builder.ConfigureServices().Configure();

    if (app.Environment.IsDevelopment())
    {
        Log.Debug("Starting Seed Data");
        using var scope = app.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<OnboardingDBContext>();
        SeedData.Initialize(dbContext);
    }

    app.Run();
}
catch(Exception ex)
{
    Log.Debug("exception from program");
    Log.Error(ex.Message, ex);
}
