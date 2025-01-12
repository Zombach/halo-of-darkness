namespace HaloOfDarkness.Server.Extensions;

internal static class HttpContextExtension
{
    public static Guid CreateCorrelationId(this HttpContext context)
    {
        var correlationId = Guid.NewGuid();
        context.Request.Headers.Append(correlationId.ToString(), correlationId.ToString());

        return correlationId;
    }
}
