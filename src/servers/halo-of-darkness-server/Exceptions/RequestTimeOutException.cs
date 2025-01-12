namespace HaloOfDarkness.Server.Exceptions;

public class RequestTimeOutException : Exception
{
    public RequestTimeOutException(string? message = default, Exception? innerException = default)
        : base(message, innerException)
    {
    }
}
