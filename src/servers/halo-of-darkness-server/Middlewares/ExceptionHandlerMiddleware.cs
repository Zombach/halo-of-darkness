using System.Text.Encodings.Web;
using System.Text.Json;
using HaloOfDarkness.Libs.Exceptions;
using HaloOfDarkness.Libs.Exceptions.Common;
using InvalidOperationException = HaloOfDarkness.Libs.Exceptions.InvalidOperationException;

namespace HaloOfDarkness.Server.Middlewares;

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
        try
        {
            await next.Invoke(context);
        }
        catch (BadRequestException exception)
        {
            await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
        }
        catch (ForbiddenException exception)
        {
            await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
        }
        catch (InternalServerErrorException exception)
        {
            await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
        }
        catch (InvalidOperationException exception)
        {
            await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
        }
        catch (NotFoundException exception)
        {
            await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
        }
        catch (OriginIsUnreachableException exception)
        {
            await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
        }
        catch (RequestTimeOutException exception)
        {
            await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
        }
        catch (ServiceUnavailableException exception)
        {
            await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
        }
        catch (UnauthorizedException exception)
        {
            await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
        }
        catch (UnprocessableEntityException exception)
        {
            await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
        }
        catch (ValidationException exception)
        {
            await HandleExceptionMessageAsync(context, exception).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            await HandleExceptionMessageAsync(context, new InternalServerErrorException(exception.Message, exception)).ConfigureAwait(false);
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
