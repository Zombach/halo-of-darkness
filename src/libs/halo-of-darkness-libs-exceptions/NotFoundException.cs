using System;
using System.Net;
using HaloOfDarkness.Libs.Exceptions.Common;

namespace HaloOfDarkness.Libs.Exceptions;

public sealed class NotFoundException
    : BaseException
{
    public override int RequestStatusCode => (int)HttpStatusCode.NotFound;

    public NotFoundException() { }

    public NotFoundException(string? message)
        : base(message) { }

    public NotFoundException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
