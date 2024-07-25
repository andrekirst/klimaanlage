using Api.Handlers.Mode;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModeController(IMediator mediator) : ControllerBase
{
    [HttpPost("select/manual/fan-full-power")]
    public async Task<IActionResult> SelectManualWithFullPowerWorkingMode(CancellationToken cancellationToken = default)
    {
        var successful = await mediator.Send(new SelectManualWithFullPowerWorkingModeRequest(), cancellationToken);
        return successful ? Ok() : NoContent();
    }

    [HttpPost("off")]
    public async Task<IActionResult> SelectOffWorkingMode(CancellationToken cancellationToken = default)
    {
        var successful = await mediator.Send(new SelectOffWorkingModeRequest(), cancellationToken);
        return successful ? Ok() : NoContent();
    }
}