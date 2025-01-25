using HaloOfDarkness.Server.Application.Authorization.Enums;
using Keycloak.AuthServices.Authorization;

namespace HaloOfDarkness.Server.Application.Authorization.Attributes;

/// <summary>
/// Specifies the class this attribute is applied to requires authorization.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeProtectedResourceAttribute
    : AuthorizeAttribute
{
    private readonly ResourceAuthorizationMode _mode;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class.
    /// </summary>
    public AuthorizeProtectedResourceAttribute(
        string resource,
        string scope,
        ResourceAuthorizationMode mode = ResourceAuthorizationMode.Resource)
    {
        _mode = mode;
        Resource = resource;
        Scope = scope;
        Policy = ProtectedResourcePolicy.From(resource, scope);
    }

    /// <summary>
    /// Gets or sets a comma delimited list of roles that are allowed to access the resource.
    /// </summary>
    public string Resource { get; init; }

    /// <summary>
    /// Gets or sets the policy name that determines access to the resource.
    /// </summary>
    public string Scope { get; init; }

    public string? ResourceId { get; set; }

    public override string? Policy
    {
        get => _mode is ResourceAuthorizationMode.ResourceFromRequest
            ? ProtectedResourcePolicy.From(Resource, ResourceId ?? string.Empty, Scope)
            : ProtectedResourcePolicy.From(Resource, Scope);
        set
        {
            // skip
        }
    }
}
