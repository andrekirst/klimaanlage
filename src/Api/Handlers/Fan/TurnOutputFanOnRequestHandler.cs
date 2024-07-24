using Api.Domain;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Handlers.Fan;

public class TurnOutputFanOnRequestHandler(
    IFanControlService fanControlService,
    IRelayControlService relayControlService) : IRequestHandler<TurnOutputFanOnRequest, IActionResult>
{
    public ValueTask<IActionResult> Handle(TurnOutputFanOnRequest request, CancellationToken cancellationToken)
    {
        relayControlService.TurnOutputRelayOn();
        fanControlService.ChangeOutputFanRotationInPercent(Percent.Max);

        return ValueTask.FromResult<IActionResult>(new OkResult());
    }
}