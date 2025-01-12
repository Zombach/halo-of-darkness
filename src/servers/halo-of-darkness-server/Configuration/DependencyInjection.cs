using System.Globalization;
using HaloOfDarkness.Server.Middleware;
using HaloOfDarkness.Server.Options;
using Serilog;
using ILogger = Serilog.ILogger;

namespace HaloOfDarkness.Server.Configuration;

internal static class DependencyInjection
{
    public static ConfigurationManager AddConfiguration(
        this ConfigurationManager configuration,
        string environment)
    {
        const string pathDefault = "appsettings.json";

        var directory = Path.GetDirectoryName(typeof(Program).Assembly.Location) ?? string.Empty;
        var basePath = Path.Combine(directory, "Configs");
        configuration.SetBasePath(basePath);

        var appSettingsPath = environment.ToLower(CultureInfo.CurrentCulture) switch
        {
            "development" => "appsettings.development.json",
            //"docker" => "appsettings.docker.json",
            _ => pathDefault,
        };

        configuration.AddAppSetting(pathDefault);

        if (!appSettingsPath.Equals(pathDefault))
        {
            configuration.AddAppSetting(appSettingsPath, optional: true);
        }

        return configuration;
    }

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

    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddOptionsWithValidate<DelayLogRequestOptions>(configuration.GetSection(DelayLogRequestOptions.SectionKey));

        return services;
    }

    public static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<CorrelationMiddleware>();
        services.AddTransient<ExceptionHandlerMiddleware>();
        services.AddTransient<RequestLoggingMiddleware>();
        services.AddTransient<RequestProfilingMiddleware>();

        return services;
    }

    public static WebApplication UseMiddleware(this WebApplication application)
    {
        application.UseMiddleware<CorrelationMiddleware>();
        application.UseMiddleware<ExceptionHandlerMiddleware>();
        application.UseMiddleware<RequestLoggingMiddleware>();
        application.UseMiddleware<RequestProfilingMiddleware>();

        return application;
    }
}
