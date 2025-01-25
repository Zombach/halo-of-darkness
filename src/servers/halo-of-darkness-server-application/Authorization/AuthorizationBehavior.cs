using HaloOfDarkness.Server.Application.Authorization.Attributes;
using HaloOfDarkness.Server.Application.Authorization.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HaloOfDarkness.Server.Application.Authorization;

internal sealed class AuthorizationBehavior<TRequest, TResponse>(
    IIdentityService identityService,
    ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    private readonly ILogger<TRequest> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // TODO: consider reflection performance impact
        var authorizeAttributes = request
            .GetType()
            .GetCustomAttributes(typeof(AuthorizeAttribute), true)
            .Cast<AuthorizeAttribute>()
            .ToList();

        if (!authorizeAttributes.Any())
        {
            return await next();
        }

        EnsureAuthorizedRoles(authorizeAttributes);

        await EnsureAuthorizedPolicies(request, authorizeAttributes);

        // User is authorized / authorization not required
        return await next();
    }

    private async Task EnsureAuthorizedPolicies(
        TRequest request,
        IEnumerable<AuthorizeAttribute> authorizeAttributes)
    {
        // Policy-based authorization
        var authorizeAttributesWithPolicies = authorizeAttributes
            .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
            .ToList();

        if (!authorizeAttributesWithPolicies.Any())
        {
            return;
        }

        var requiredPolicies = authorizeAttributesWithPolicies.Select(attribute =>
        {
            if (attribute is AuthorizeProtectedResourceAttribute resourceAttribute
                && request is IRequestWithResourceId requestWithResourceId)
            {
                resourceAttribute.ResourceId = requestWithResourceId.ResourceId;
            }

            return attribute.Policy;
        });

        foreach (var policy in requiredPolicies)
        {
            var authorized = await _identityService.AuthorizeAsync(
                _identityService.Principal!,
                policy!);

            if (authorized)
            {
                continue;
            }

            _logger.LogDebug("Failed policy authorization {Policy}", policy);
            throw new ForbiddenAccessException();
        }
    }

    private void EnsureAuthorizedRoles(IEnumerable<AuthorizeAttribute> authorizeAttributes)
    {
        // Role-based authorization
        var authorizeAttributesWithRoles = authorizeAttributes
            .Where(a => !string.IsNullOrWhiteSpace(a.Roles))
            .ToList();

        if (!authorizeAttributesWithRoles.Any())
        {
            return;
        }

        var requiredRoles = authorizeAttributesWithRoles
            .Where(a => !string.IsNullOrWhiteSpace(a.Roles))
            .Select(a => a.Roles!.Split(','));

        var isInRoles = requiredRoles
            .Select(roles => roles
                .Any(role => _identityService
                    .IsInRoleAsync(role.Trim())));

        if (isInRoles.Any(authorized => !authorized))
        {
            _logger.LogDebug("Failed role authorization");
            throw new ForbiddenAccessException();
        }
    }
}
