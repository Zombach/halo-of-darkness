using System;
using System.Net;
using HaloOfDarkness.Shared.Exceptions.Common;

namespace HaloOfDarkness.Shared.Exceptions;

public sealed class ServiceUnavailableException
    : BaseException
{
    public override int RequestStatusCode => (int)HttpStatusCode.ServiceUnavailable;

    public ServiceUnavailableException() { }

    public ServiceUnavailableException(string? message)
        : base(message) { }

    public ServiceUnavailableException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
