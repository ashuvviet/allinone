using moviebooking_api;
using Serilog;

Log.Logger = new LogConfiguration().CreateLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host
        .ConfigureAppConfiguration(delegate (HostBuilderContext ctx, IConfigurationBuilder configuration)
        {
            configuration.AddEnvironmentVariables();
        })
        .UseSerilog();

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    Log.Information("Seeding database...");
    SeedData.EnsureSeedData(app);
    Log.Information("Done seeding database. Exiting.");

    app.Run();
}
catch (Exception ex) when (
    // https://github.com/dotnet/runtime/issues/60600
    ex.GetType().Name is not "StopTheHostException"
    // HostAbortedException was added in .NET 7, but since we target .NET 6 we
    // need to do it this way until we target .NET 8
    && ex.GetType().Name is not "HostAbortedException")
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
