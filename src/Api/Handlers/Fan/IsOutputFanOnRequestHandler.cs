using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Handlers.Fan;

public class IsOutputFanOnRequestHandler(IFanControlService fanControlService) : IRequestHandler<IsOutputFanOnRequest, IActionResult>
{
    public ValueTask<IActionResult> Handle(IsOutputFanOnRequest request, CancellationToken cancellationToken)
    {
        var value = fanControlService.IsOutputFanOn();
        return ValueTask.FromResult<IActionResult>(new OkObjectResult(value));
    }
}