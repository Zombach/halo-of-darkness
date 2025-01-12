using System.Diagnostics;
using HaloOfDarkness.Server.Options;
using Microsoft.Extensions.Options;

namespace HaloOfDarkness.Server.Middlewares;

internal sealed class RequestProfilingMiddleware(
    ILogger<RequestProfilingMiddleware> logger,
    IOptionsSnapshot<DelayLogRequestOptions> options)
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
