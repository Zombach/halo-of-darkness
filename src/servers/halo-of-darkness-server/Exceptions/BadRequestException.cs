namespace HaloOfDarkness.Server.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string? message = default)
        : base(message)
    {
    }
}
