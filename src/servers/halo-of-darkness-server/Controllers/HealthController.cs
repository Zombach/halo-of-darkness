using HaloOfDarkness.Server.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace HaloOfDarkness.Server.Controllers;

public sealed class HealthController : BaseController
{
    [HttpGet]
    [Route("")]
    public string Test()
    {
        return $"{DateTime.Now:s}";
    }
}
