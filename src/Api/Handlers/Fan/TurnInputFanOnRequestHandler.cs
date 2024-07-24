using Api.Domain;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Handlers.Fan;

public class TurnInputFanOnRequestHandler(
    IFanControlService fanControlService,
    IRelayControlService relayControlService) : IRequestHandler<TurnInputFanOnRequest, IActionResult>
{
    public ValueTask<IActionResult> Handle(TurnInputFanOnRequest request, CancellationToken cancellationToken)
    {
        relayControlService.TurnInputRelayOn();
        fanControlService.ChangeInputFanRotationInPercent(Percent.Of(1));

        return ValueTask.FromResult<IActionResult>(new OkResult());
    }
}