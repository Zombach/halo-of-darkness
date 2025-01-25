using System.Security.Claims;

namespace HaloOfDarkness.Server.Application.Authorization.Common;

public interface ICurrentUserService
{
    string? UserId { get; }

    string? UserName { get; }

    ClaimsPrincipal? Principal { get; }
}
