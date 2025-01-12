using System.Globalization;
using System.Text;

namespace HaloOfDarkness.Server.Middlewares;

internal sealed class RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var request = context.Request;

        var task = request.Method.ToLower(CultureInfo.CurrentCulture) switch
        {
            "post" => PostRequest(request),
            _ => BaseRequest(request)
        };

        await task;
        await next(context);

        logger.LogInformation("Code: {@statusCode}",
            context.Response.StatusCode);
    }

    private Task BaseRequest(HttpRequest request)
    {
        logger.LogInformation(
            "{@method} {@url}",
            request.Method,
            request.Path.Value);

        return Task.CompletedTask;
    }

    private async Task PostRequest(HttpRequest request)
    {
        try
        {
            request.EnableBuffering();
            request.Body.Position = 0;

            using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 4096, true);
            var bodySource = await reader.ReadToEndAsync();

            logger.LogInformation(
                "{@method} {@url}, body {@request}",
                request.Method,
                request.Path.Value,
                bodySource.Replace("\n", string.Empty)
                    .Replace("\r", string.Empty)
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Log post request error: {@message}",
                ex.Message);
        }
        finally
        {
            request.Body.Position = 0;
        }
    }
}
