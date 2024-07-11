using Serilog;
using Serilog.Formatting.Compact;
using System.Reflection;
using System.Reflection.Emit;

namespace moviebooking_api
{
    public class LogConfiguration
    {
        private const string Debug = "debug";
        private const string Warning = "warning";
        private const string Error = "error";
        private const string logLevelVariable = "LOG_LEVEL";
        private const string logTemplateVariable = "LOG_TEMPLATE";

        public LoggerConfiguration LoggerConfiguration
        {
            get
            {
                var logger = new LoggerConfiguration();
                var logLevel = Environment.GetEnvironmentVariable(logLevelVariable);
                if (string.IsNullOrEmpty(logLevel))
                {
                    //Setting default loglevel as Information
                    logger = logger.MinimumLevel.Information();
                }
                else
                {
                    logger = logLevel.ToLowerInvariant() switch
                    {
                        Debug => logger.MinimumLevel.Debug(),
                        Warning => logger.MinimumLevel.Warning(),
                        Error => logger.MinimumLevel.Error(),
                        _ => logger.MinimumLevel.Information(),
                    };
                }
                logger
                    .Enrich.WithProperty("System", "moviebooking")
                    .Enrich.WithProperty("Version", Assembly.GetExecutingAssembly().GetName().Version?.ToString())
                    .Enrich.FromLogContext();

                UpdateLogTemplate(logger);
                return logger;
            }
        }

        private void UpdateLogTemplate(LoggerConfiguration logger)
        {
            var template = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{SourceContext}] [{EventId}] {Message}{NewLine}{Exception}"; //Environment.GetEnvironmentVariable(logTemplateVariable);
            if (string.IsNullOrEmpty(template))
            {
                logger.WriteTo.Console(new CompactJsonFormatter());
            }
            else
            {
                logger.WriteTo.Console(outputTemplate: template);
            }
        }

        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <returns></returns>
        public Serilog.ILogger CreateLogger()
        {
            return LoggerConfiguration.CreateLogger();
        }
    }
}
