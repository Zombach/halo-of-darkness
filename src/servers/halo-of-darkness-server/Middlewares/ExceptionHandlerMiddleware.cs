using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using HaloOfDarkness.Server.Exceptions;

namespace HaloOfDarkness.Server.Middlewares;

public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    : IMiddleware
{
    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (ForbiddenException exception)
        {
            await HandleExceptionMessageAsync(context, exception, HttpStatusCode.Forbidden).ConfigureAwait(false);
        }
        catch (BadRequestException exception)
        {
            await HandleExceptionMessageAsync(context, exception, HttpStatusCode.BadRequest).ConfigureAwait(false);
        }
        catch (RequestTimeOutException exception)
        {
            await HandleExceptionMessageAsync(context, exception, HttpStatusCode.RequestTimeout).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            await HandleExceptionMessageAsync(context, exception, HttpStatusCode.InternalServerError).ConfigureAwait(false);
        }
    }

    private Task HandleExceptionMessageAsync(HttpContext context, Exception exception, HttpStatusCode code)
    {
        logger.LogError(exception, "StatusCode: {@statusCode} Message: {@message}",
            code,
            exception.Message);

        var statusCode = (int)code;
        var result = JsonSerializer.Serialize(
            new { StatusCode = statusCode, ErrorMessage = exception.Message },
            s_jsonSerializerOptions);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(result);
    }
}
