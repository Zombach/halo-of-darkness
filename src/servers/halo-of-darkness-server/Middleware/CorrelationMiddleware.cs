using Serilog.Context;

namespace HaloOfDarkness.Server.Middleware;

internal sealed class CorrelationMiddleware : IMiddleware
{
    private const string CorrelationId = nameof(CorrelationId);

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var correlationId = Guid.NewGuid();
        //context.Request.Headers.Append(CorrelationId, correlationId.ToString());
        using (LogContext.PushProperty(CorrelationId, correlationId))
        {
            await next.Invoke(context);
        }
    }
}
