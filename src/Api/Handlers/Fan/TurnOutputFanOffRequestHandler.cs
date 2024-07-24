using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Handlers.Fan;

public class TurnOutputFanOffRequestHandler(IRelayControlService relaisControlService) : IRequestHandler<TurnOutputFanOffRequest, IActionResult>
{
    public ValueTask<IActionResult> Handle(TurnOutputFanOffRequest request, CancellationToken cancellationToken)
    {
        relaisControlService.TurnOutputRelayOff();
        return ValueTask.FromResult<IActionResult>(new OkResult());
    }
}