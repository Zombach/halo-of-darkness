using HaloOfDarkness.Server.Application.Authorization;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HaloOfDarkness.Server.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

        return services;
    }
}
