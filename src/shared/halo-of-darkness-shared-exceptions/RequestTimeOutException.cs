using System;
using System.Net;
using HaloOfDarkness.Shared.Exceptions.Common;

namespace HaloOfDarkness.Shared.Exceptions;

public sealed class RequestTimeOutException
    : BaseException
{
    public override int RequestStatusCode => (int)HttpStatusCode.RequestTimeout;

    public RequestTimeOutException() { }

    public RequestTimeOutException(string? message)
        : base(message) { }

    public RequestTimeOutException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
