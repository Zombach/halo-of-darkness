using System;
using System.Net;
using HaloOfDarkness.Libs.Exceptions.Common;

namespace HaloOfDarkness.Libs.Exceptions;

public sealed class InvalidOperationException
    : BaseException
{
    public override int RequestStatusCode => (int)HttpStatusCode.BadRequest;

    public InvalidOperationException() { }

    public InvalidOperationException(string? message)
        : base(message) { }

    public InvalidOperationException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
