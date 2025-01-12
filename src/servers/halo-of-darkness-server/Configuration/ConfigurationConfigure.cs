using System.Globalization;

namespace HaloOfDarkness.Server.Configuration;

internal static class ConfigurationConfigure
{
    public static ConfigurationManager AddConfiguration
    (
        this ConfigurationManager configuration,
        string environment
    )
    {
        var directory = Path.GetDirectoryName(typeof(Program).Assembly.Location) ?? string.Empty;
        var basePath = Path.Combine(directory, "Configs");
        configuration.SetBasePath(basePath);

        configuration.AddAppSettings(optional: false);
        configuration.AddAppSettings(environment);

        return configuration;
    }

    private static IConfigurationBuilder AddAppSettings(
        this IConfigurationBuilder configuration,
        string environment = "",
        bool optional = true,
        bool reloadOnChange = true)
    {
        var appSettingsPath = environment.ToLower(CultureInfo.CurrentCulture) switch
        {
            "development" => "appsettings.development.json",
            //"docker" => "appsettings.docker.json",
            _ => "appsettings.json",
        };

        return configuration.AddJsonFile
        (
            appSettingsPath,
            optional: optional,
            reloadOnChange: reloadOnChange
        );
    }
}
