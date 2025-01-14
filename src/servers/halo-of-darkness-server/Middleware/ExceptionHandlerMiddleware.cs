using System.Text.Encodings.Web;
using System.Text.Json;
using HaloOfDarkness.Shared.Exceptions;
using HaloOfDarkness.Shared.Exceptions.Common;
using InvalidOperationException = HaloOfDarkness.Shared.Exceptions.InvalidOperationException;

namespace HaloOfDarkness.Server.Middleware;

internal sealed class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    : IMiddleware
{
    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        BaseException? baseException = default;
        try
        {
            await next.Invoke(context);
        }
        catch (BadRequestException exception)
        {
            baseException = exception;
        }
        catch (ForbiddenException exception)
        {
            baseException = exception;
        }
        catch (InternalServerErrorException exception)
        {
            baseException = exception;
        }
        catch (InvalidOperationException exception)
        {
            baseException = exception;
        }
        catch (NotFoundException exception)
        {
            baseException = exception;
        }
        catch (OriginIsUnreachableException exception)
        {
            baseException = exception;
        }
        catch (RequestTimeOutException exception)
        {
            baseException = exception;
        }
        catch (ServiceUnavailableException exception)
        {
            baseException = exception;
        }
        catch (UnauthorizedException exception)
        {
            baseException = exception;
        }
        catch (UnprocessableEntityException exception)
        {
            baseException = exception;
        }
        catch (ValidationException exception)
        {
            baseException = exception;
        }
        catch (Exception exception)
        {
            baseException = new InternalServerErrorException(exception.Message, exception);
        }

        if (baseException is not null)
        {
            await HandleExceptionMessageAsync(context, baseException).ConfigureAwait(false);
        }
    }

    private Task HandleExceptionMessageAsync(HttpContext context, BaseException exception)
    {
        logger.LogError(exception, "StatusCode: {@statusCode} Message: {@message}",
            exception.RequestStatusCode,
            exception.Message);

        var result = JsonSerializer.Serialize(
            new { StatusCode = exception.RequestStatusCode, ErrorMessage = exception.Message },
            s_jsonSerializerOptions);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.RequestStatusCode;
        return context.Response.WriteAsync(result);
    }
}
