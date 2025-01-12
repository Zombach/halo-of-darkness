using System;

namespace HaloOfDarkness.Libs.Exceptions.Common;

public abstract class BaseException
    : Exception
{
    public abstract int RequestStatusCode { get; }

    protected BaseException() { }

    protected BaseException(string? message)
        : base(message) { }

    protected BaseException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
