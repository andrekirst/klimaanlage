using System.Net;
using Api.Handlers.Fan;
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
    public ValueTask<IActionResult> InputFanStatusOn(CancellationToken cancellationToken = default)
        => mediator.Send(new IsInputFanOnRequest(), cancellationToken);

    [HttpPost("output/off")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public ValueTask<IActionResult> TurnOutputFanOff(CancellationToken cancellationToken = default)
        => mediator.Send(new TurnOutputFanOffRequest(), cancellationToken);

    [HttpPost("output/on")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public ValueTask<IActionResult> TurnOutputFanOn(CancellationToken cancellationToken = default)
        => mediator.Send(new TurnOutputFanOnRequest(), cancellationToken);

    [HttpGet("output/status/on")]
    [ProducesResponseType<bool>((int)HttpStatusCode.OK)]
    public ValueTask<IActionResult> OutputFanStatusOn(CancellationToken cancellationToken = default)
        => mediator.Send(new IsOutputFanOnRequest(), cancellationToken);
}