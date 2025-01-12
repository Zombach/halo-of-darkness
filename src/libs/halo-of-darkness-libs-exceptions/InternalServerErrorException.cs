using System;
using System.Net;
using HaloOfDarkness.Libs.Exceptions.Common;

namespace HaloOfDarkness.Libs.Exceptions;

public sealed class InternalServerErrorException
    : BaseException
{
    public override int RequestStatusCode => (int)HttpStatusCode.InternalServerError;

    public InternalServerErrorException() { }

    public InternalServerErrorException(string? message)
        : base(message) { }

    public InternalServerErrorException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
