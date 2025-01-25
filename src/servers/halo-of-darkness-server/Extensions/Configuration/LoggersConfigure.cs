using HaloOfDarkness.Server.Configuration;
using Serilog;
using ILogger = Serilog.ILogger;

namespace HaloOfDarkness.Server.Extensions.Configuration;

internal static class LoggersConfigure
{
    public static void AddLoggers(
        this WebApplicationBuilder builder,
        out ILogger firstLogger,
        params string[] sectionKeys)
    {
        var loggingBuilder = builder.Logging;
        loggingBuilder.ClearProviders();

        if (sectionKeys.Length is 0)
        {
            var defaultLogger = LoggingConfiguration.CreateDefaultLogger();
            builder.Logging.AddSerilog(defaultLogger);
            firstLogger = defaultLogger;
            return;
        }

        var loggers = sectionKeys.Select(sectionKey
                => LoggingConfiguration.CreateLogger(builder.Configuration, sectionKey))
            .ToList();

        loggers.ForEach(logger => builder.Logging.AddSerilog(logger));

        firstLogger = loggers.First();
    }
}
