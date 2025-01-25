using System;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using HaloOfDarkness.Server.Application.Authorization.Common;
using Microsoft.AspNetCore.Authorization;

namespace HaloOfDarkness.Server.Infrastructure.Authorization;

internal sealed class IdentityService(
    IAuthorizationService authorizationService,
    ICurrentUserService userService)
    : IIdentityService
{
    private readonly IAuthorizationService _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
    private readonly ICurrentUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));

    #region ICurrentUserService

    public string? UserId => _userService.UserId;

    public string? UserName => _userService.UserName;

    public ClaimsPrincipal? Principal => _userService.Principal;

    #endregion

    public Task<bool> AuthorizeAsync(string policyName)
    {
        var principal = GetPrincipal();
        return AuthorizeAsync(principal, policyName);
    }

    public async Task<bool> AuthorizeAsync(object resource, string policyName)
    {
        var principal = GetPrincipal();
        var result = await _authorizationService
            .AuthorizeAsync(principal, resource, policyName);

        return result.Succeeded;
    }

    public bool IsInRoleAsync(string role) => Principal?.IsInRole(role) ?? false;

    private async Task<bool> AuthorizeAsync(ClaimsPrincipal principal, string policyName)
    {
        var result = await _authorizationService
            .AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    private ClaimsPrincipal GetPrincipal()
    {
        var principal = _userService.Principal
                        ?? throw new AuthenticationException("Couldn't find principal. Please authenticate");
        return principal;
    }
}
