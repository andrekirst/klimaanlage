using Api.Services;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Api.Handlers.Fan;

public class TurnOffRequestHandler(IRelayControlService relaisControlService) : IRequestHandler<TurnInputFanOffRequest, IActionResult>
{
    public ValueTask<IActionResult> Handle(TurnInputFanOffRequest request, CancellationToken cancellationToken)
    {
        relaisControlService.TurnInputRelayOff();
        return ValueTask.FromResult<IActionResult>(new OkResult());
    }
}