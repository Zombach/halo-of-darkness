using HaloOfDarkness.Server.Controllers.Common;

namespace HaloOfDarkness.Server.Controllers;

using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Protection;
using Keycloak.AuthServices.Sdk.Protection.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/keycloak-api")]
public sealed class KeycloakAdminController(
    IKeycloakRealmClient keycloakRealmClient,
    IKeycloakProtectionClient protectionClient)
    : BaseController
{
    private const string DefaultRealm = "authz";

    [HttpGet]
    [Route("realms")]
    public async Task<IActionResult> GetRealms()
    {
        return Ok(await keycloakRealmClient.GetRealmAsync(DefaultRealm));
    }

    [HttpGet]
    [Route("resources")]
    public async Task<IActionResult> GetResources()
    {
        return Ok(await protectionClient.GetResourcesAsync(DefaultRealm));
    }

    [HttpGet]
    [Route("resources/{id}")]
    public async Task<IActionResult> GetResource(string id)
    {
        return Ok(await protectionClient.GetResourceAsync(DefaultRealm, id));
    }

    [HttpPost]
    [Route("resources")]
    public async Task<IActionResult> CreateResource()
    {
        var resource = new Resource(
            $"workspaces/{Guid.NewGuid()}",
            ["workspaces:read", "workspaces:delete"]
        )
        {
            Attributes = { ["test"] = "Owner, Operations" },
            Type = "urn:workspace-authz:resource:workspaces",
        };
        return Ok(await protectionClient.CreateResourceAsync(DefaultRealm, resource));
    }
}
