using System.ComponentModel.DataAnnotations;

namespace HaloOfDarkness.Server.Options;

internal sealed class RequestProfilingOptions
{
    public const string SectionKey = nameof(RequestProfilingOptions);

    [Required]
    public required int DelayMilliseconds { get; init; }
}
