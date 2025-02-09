using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace HaloOfDarkness.IntegrationTests.Abstraction.Extensions;

public static class ServiceCollectionServiceExtensions
{
    public static IServiceCollection ReplaceServiceWithSingletonMock<TService>(this IServiceCollection services)
        where TService : class
    {
        services.RemoveAll(typeof(TService));
        services.AddSingleton(_ => Mock.Of<TService>());
        return services;
    }
}
