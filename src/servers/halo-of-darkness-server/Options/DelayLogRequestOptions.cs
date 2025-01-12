using System.ComponentModel.DataAnnotations;

namespace HaloOfDarkness.Server.Options;

internal sealed class DelayLogRequestOptions
{
    public const string SectionKey = nameof(DelayLogRequestOptions);

    [Required]
    public required int DelayMilliseconds { get; init; }
}
