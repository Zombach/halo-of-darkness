using System;
using HaloOfDarkness.Libs.Exceptions.Common;

namespace HaloOfDarkness.Libs.Exceptions;

public sealed class OriginIsUnreachableException
    : BaseException
{
    public override int RequestStatusCode => 523;

    public OriginIsUnreachableException() { }

    public OriginIsUnreachableException(string? message)
        : base(message) { }

    public OriginIsUnreachableException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
