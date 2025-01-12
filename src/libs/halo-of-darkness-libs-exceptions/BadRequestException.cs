using System;
using System.Net;
using HaloOfDarkness.Libs.Exceptions.Common;

namespace HaloOfDarkness.Libs.Exceptions;

public sealed class BadRequestException
    : BaseException
{
    public override int RequestStatusCode => (int)HttpStatusCode.BadRequest;

    public BadRequestException() { }

    public BadRequestException(string? message)
        : base(message) { }

    public BadRequestException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
