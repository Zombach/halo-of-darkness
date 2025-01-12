using System;
using System.Net;
using HaloOfDarkness.Shared.Exceptions.Common;

namespace HaloOfDarkness.Shared.Exceptions;

public sealed class ForbiddenException
    : BaseException
{
    public override int RequestStatusCode => (int)HttpStatusCode.Forbidden;

    public ForbiddenException() { }

    public ForbiddenException(string? message)
        : base(message) { }

    public ForbiddenException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
