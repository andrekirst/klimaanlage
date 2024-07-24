using System.Net;
using Api.Handlers.Fan;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FanController(IMediator mediator) : ControllerBase
{
    [HttpPost("input/off")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public ValueTask<IActionResult> TurnInputFanOff(CancellationToken cancellationToken = default)
        => mediator.Send(new TurnInputFanOffRequest(), cancellationToken);

    [HttpPost("input/on")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public ValueTask<IActionResult> TurnInputFanOn(CancellationToken cancellationToken = default)
        => mediator.Send(new TurnInputFanOnRequest(), cancellationToken);

    [HttpGet("input/status/on")]
    [ProducesResponseType<bool>((int)HttpStatusCode.OK)]
    public ValueTask<IActionResult> StatusOn(CancellationToken cancellationToken = default)
        => mediator.Send(new IsInputFanOnRequest(), cancellationToken);
}