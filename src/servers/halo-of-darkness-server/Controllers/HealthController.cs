using HaloOfDarkness.Server.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaloOfDarkness.Server.Controllers;

[Authorize]
public sealed class HealthController : BaseController
{
    [HttpGet]
    [Route("")]
    public string Test()
    {
        return $"{DateTime.Now:s}";
    }
}
