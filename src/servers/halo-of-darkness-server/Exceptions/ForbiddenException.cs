namespace HaloOfDarkness.Server.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string? message = default)
        : base(message)
    {
    }
}
