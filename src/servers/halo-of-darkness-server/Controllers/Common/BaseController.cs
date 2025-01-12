using Microsoft.AspNetCore.Mvc;

namespace HaloOfDarkness.Server.Controllers.Common;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase;
