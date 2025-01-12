using System;
using System.Net;
using HaloOfDarkness.Shared.Exceptions.Common;

namespace HaloOfDarkness.Shared.Exceptions;

public sealed class UnprocessableEntityException
    : BaseException
{
    public override int RequestStatusCode => (int)HttpStatusCode.UnprocessableEntity;

    public UnprocessableEntityException() { }

    public UnprocessableEntityException(string? message)
        : base(message) { }

    public UnprocessableEntityException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
