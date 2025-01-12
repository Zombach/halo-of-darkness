using Serilog;
using Serilog.Settings.Configuration;
using ILogger = Serilog.ILogger;

namespace HaloOfDarkness.Server.Configuration;

internal static class LoggingConfiguration
{
    public static ILogger AddLoggers(
        this ILoggingBuilder builder,
        IConfiguration configuration,
        params string[] sectionNames)
    {
        ILogger? defaultLogger = default;
        foreach (var sectionName in sectionNames)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(
                    configuration,
                    new ConfigurationReaderOptions { SectionName = sectionName })
                .Enrich.FromLogContext()
                .CreateBootstrapLogger();

            defaultLogger ??= logger;
            builder.AddSerilog(logger);
        }

        ArgumentNullException.ThrowIfNull(defaultLogger);

        return defaultLogger!;
    }
}
