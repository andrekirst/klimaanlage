using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModeController(IMediator mediator) : ControllerBase
{
    [HttpPost("select/manual/fanspeed")]
    public async Task<IActionResult> SelectManualWithFanSpeedWorkingMode(double percent, CancellationToken cancellationToken = default)
    {

        return Ok();
    }
}