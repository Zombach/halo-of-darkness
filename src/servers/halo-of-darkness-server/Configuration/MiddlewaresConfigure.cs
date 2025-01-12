using HaloOfDarkness.Server.Middlewares;

namespace HaloOfDarkness.Server.Configuration;

internal static class MiddlewaresConfigure
{
    public static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<CorrelationMiddleware>();
        services.AddTransient<ExceptionHandlerMiddleware>();
        services.AddTransient<RequestLoggingMiddleware>();
        services.AddTransient<RequestProfilingMiddleware>();

        return services;
    }

    public static WebApplication UseMiddlewares(this WebApplication application)
    {
        application.UseMiddleware<CorrelationMiddleware>();
        application.UseMiddleware<ExceptionHandlerMiddleware>();
        application.UseMiddleware<RequestLoggingMiddleware>();
        application.UseMiddleware<RequestProfilingMiddleware>();

        return application;
    }
}
