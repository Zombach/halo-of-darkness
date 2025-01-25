namespace HaloOfDarkness.Server.Application.Authorization.Common;

public interface IIdentityService
    : ICurrentUserService
{
    Task<bool> AuthorizeAsync(string policyName);

    Task<bool> AuthorizeAsync(object resource, string policyName);

    bool IsInRoleAsync(string role);
}
