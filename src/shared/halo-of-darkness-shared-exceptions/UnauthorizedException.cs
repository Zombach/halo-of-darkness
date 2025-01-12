using System;
using System.Net;
using HaloOfDarkness.Shared.Exceptions.Common;

namespace HaloOfDarkness.Shared.Exceptions;

public sealed class UnauthorizedException
    : BaseException
{
    public override int RequestStatusCode => (int)HttpStatusCode.Unauthorized;

    public UnauthorizedException() { }

    public UnauthorizedException(string? message)
        : base(message) { }

    public UnauthorizedException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
