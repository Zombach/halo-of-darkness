using HaloOfDarkness.Server.Controllers.Common;

namespace HaloOfDarkness.Server.Controllers;

using Keycloak.AuthServices.Authorization.AuthorizationServer;
using Microsoft.AspNetCore.Mvc;

[Route("api/authz")]
public sealed class KeycloakAuthZController(IAuthorizationServerClient protectionClient)
    : BaseController
{
    [HttpGet]
    [Route("try-resource")]
    public async Task<IActionResult> VerifyAccess(
        [FromQuery] string? resource,
        [FromQuery] string? scope,
        CancellationToken cancellationToken)
    {
        var verified = await protectionClient
            .VerifyAccessToResource(
                resource ?? "workspaces",
                scope ?? "workspaces:read",
                cancellationToken);

        return Ok(verified);
    }
}
