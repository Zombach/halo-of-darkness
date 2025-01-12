using HaloOfDarkness.Server.Extensions;
using Serilog.Context;

namespace HaloOfDarkness.Server.Middlewares;

internal sealed class CorrelationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.PushProperty("CorrelationId", context.CreateCorrelationId()))
        {
            await next.Invoke(context);
        }
    }
}
