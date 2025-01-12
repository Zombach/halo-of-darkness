using System;
using System.Net;
using HaloOfDarkness.Shared.Exceptions.Common;

namespace HaloOfDarkness.Shared.Exceptions;

public sealed class ValidationException
    : BaseException
{
    public override int RequestStatusCode => (int)HttpStatusCode.BadRequest;

    public ValidationException() { }

    public ValidationException(string? message)
        : base(message) { }

    public ValidationException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
