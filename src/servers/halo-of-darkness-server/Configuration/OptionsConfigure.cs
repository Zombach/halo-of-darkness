using HaloOfDarkness.Server.Options;

namespace HaloOfDarkness.Server.Configuration;

public static class OptionsConfigure
{
    public static IServiceCollection AddOptions
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddOptionsWithValidate<DelayLogRequestOptions>(configuration.GetSection(DelayLogRequestOptions.SectionKey));

        return services;
    }

    private static void AddOptionsWithValidate<T>(
        this IServiceCollection services,
        IConfigurationSection configurationSection) where T : class
    {
        services.AddOptionsWithValidateOnStart<T>()
            .Bind(configurationSection)
            .ValidateDataAnnotations();
    }
}
