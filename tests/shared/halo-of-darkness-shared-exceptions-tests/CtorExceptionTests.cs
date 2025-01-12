using System.Net;
using FluentAssertions;
using HaloOfDarkness.Shared.Exceptions.Common;
using HaloOfDarkness.Shared.Exceptions.Tests.Configuration;

namespace HaloOfDarkness.Shared.Exceptions.Tests;

public sealed class CtorExceptionTests
{
    [Theory, CustomAutoData]
    public void CtorBadRequestExceptionSuccessfully(string message, Exception innerException)
    {
        CtorException(
            (int)HttpStatusCode.BadRequest,
            message,
            innerException,
            () => new BadRequestException(message),
            () => new BadRequestException(message, innerException));
    }

    [Theory, CustomAutoData]
    public void CtorForbiddenExceptionSuccessfully(string message, Exception innerException)
    {
        CtorException(
            (int)HttpStatusCode.Forbidden,
            message,
            innerException,
            () => new ForbiddenException(message),
            () => new ForbiddenException(message, innerException));
    }

    [Theory, CustomAutoData]
    public void CtorInternalServerErrorExceptionSuccessfully(string message, Exception innerException)
    {
        CtorException(
            (int)HttpStatusCode.InternalServerError,
            message,
            innerException,
            () => new InternalServerErrorException(message),
            () => new InternalServerErrorException(message, innerException));
    }

    [Theory, CustomAutoData]
    public void CtorInvalidOperationExceptionSuccessfully(string message, Exception innerException)
    {
        CtorException(
            (int)HttpStatusCode.BadRequest,
            message,
            innerException,
            () => new InvalidOperationException(message),
            () => new InvalidOperationException(message, innerException));
    }

    [Theory, CustomAutoData]
    public void CtorNotFoundExceptionSuccessfully(string message, Exception innerException)
    {
        CtorException(
            (int)HttpStatusCode.NotFound,
            message,
            innerException,
            () => new NotFoundException(message),
            () => new NotFoundException(message, innerException));
    }

    [Theory, CustomAutoData]
    public void CtorOriginIsUnreachableExceptionSuccessfully(string message, Exception innerException)
    {
        const int originIsUnreachable = 523;
        CtorException(
            originIsUnreachable,
            message,
            innerException,
            () => new OriginIsUnreachableException(message),
            () => new OriginIsUnreachableException(message, innerException));
    }

    [Theory, CustomAutoData]
    public void CtorRequestTimeOutExceptionSuccessfully(string message, Exception innerException)
    {
        CtorException(
            (int)HttpStatusCode.RequestTimeout,
            message,
            innerException,
            () => new RequestTimeOutException(message),
            () => new RequestTimeOutException(message, innerException));
    }

    [Theory, CustomAutoData]
    public void CtorServiceUnavailableExceptionSuccessfully(string message, Exception innerException)
    {
        CtorException(
            (int)HttpStatusCode.ServiceUnavailable,
            message,
            innerException,
            () => new ServiceUnavailableException(message),
            () => new ServiceUnavailableException(message, innerException));
    }

    [Theory, CustomAutoData]
    public void CtorUnauthorizedExceptionSuccessfully(string message, Exception innerException)
    {
        CtorException(
            (int)HttpStatusCode.Unauthorized,
            message,
            innerException,
            () => new UnauthorizedException(message),
            () => new UnauthorizedException(message, innerException));
    }

    [Theory, CustomAutoData]
    public void CtorUnprocessableEntityExceptionSuccessfully(string message, Exception innerException)
    {
        CtorException(
            (int)HttpStatusCode.UnprocessableEntity,
            message,
            innerException,
            () => new UnprocessableEntityException(message),
            () => new UnprocessableEntityException(message, innerException));
    }

    [Theory, CustomAutoData]
    public void CtorValidationExceptionSuccessfully(string message, Exception innerException)
    {
        CtorException(
            (int)HttpStatusCode.BadRequest,
            message,
            innerException,
            () => new ValidationException(message),
            () => new ValidationException(message, innerException));
    }

    private void CtorException<TException>(
        int statusCode,
        string message,
        Exception innerException,
        Func<TException> funcCtorWithMessage,
        Func<TException> funcCtorWithMessageAndInnerException)
        where TException : BaseException, new()
    {
        var exception = new TException();
        var exceptionWithMessage = funcCtorWithMessage();
        var exceptionWithMessageAndInnerException = funcCtorWithMessageAndInnerException();

        exception.RequestStatusCode.Should().Be(statusCode);
        exception.Message.Should().Be($"Exception of type '{typeof(TException).FullName}' was thrown.");
        exception.InnerException.Should().BeNull();

        exceptionWithMessage.RequestStatusCode.Should().Be(statusCode);
        exceptionWithMessage.Message.Should().Be(message);
        exceptionWithMessage.InnerException.Should().BeNull();

        exceptionWithMessageAndInnerException.RequestStatusCode.Should().Be(statusCode);
        exceptionWithMessageAndInnerException.Message.Should().Be(message);
        exceptionWithMessageAndInnerException.InnerException.Should().Be(innerException);
        exceptionWithMessageAndInnerException.InnerException?.Message.Should().Be(innerException.Message);
    }
}
