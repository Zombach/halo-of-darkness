using System.Diagnostics;
using HaloOfDarkness.Server.Options;
using Microsoft.Extensions.Options;

namespace HaloOfDarkness.Server.Middleware;

internal sealed class RequestProfilingMiddleware(
    ILogger<RequestProfilingMiddleware> logger,
    IOptionsSnapshot<RequestProfilingOptions> options)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        await next(context);

        stopwatch.Stop();

        if (stopwatch.ElapsedMilliseconds > TimeSpan.FromMilliseconds(options.Value.DelayMilliseconds).TotalMilliseconds)
        {
            logger.LogInformation("request to slow {@milliseconds} milliseconds",
                stopwatch.ElapsedMilliseconds);
        }
    }
}
