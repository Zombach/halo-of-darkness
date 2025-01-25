using System.Security.Claims;
using HaloOfDarkness.Server.Application.Authorization.Common;
using Microsoft.AspNetCore.Http;

namespace HaloOfDarkness.Server.Infrastructure.Authorization;

internal sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor)
    : ICurrentUserService
{
    public string? UserId => httpContextAccessor
        .HttpContext
        ?.User
        .FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserName => httpContextAccessor
        .HttpContext
        ?.User
        .FindFirstValue("preferred_username");

    public ClaimsPrincipal? Principal => httpContextAccessor
        .HttpContext
        ?.User;
}
