using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HaloOfDarkness.Libs.Configuration;

public static class OptionsConfigure
{
    public static IServiceCollection AddOptionsWithValidate<TOptions>(
        this IServiceCollection services,
        IConfigurationSection configurationSection)
        where TOptions : class
    {
        services.AddOptionsWithValidateOnStart<TOptions>()
            .Bind(configurationSection)
            .ValidateDataAnnotations();

        return services;
    }
}
