using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HaloOfDarkness.Server.Controllers.Common;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private ISender? _sender;

    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
