using Api.Services;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Api.Handlers.Fan;

public class IsInputFanOnRequestHandler(IFanControlService fanControlService) : IRequestHandler<IsInputFanOnRequest, IActionResult>
{
    public ValueTask<IActionResult> Handle(IsInputFanOnRequest request, CancellationToken cancellationToken)
    {
        var value = fanControlService.IsInputFanOn();
        return ValueTask.FromResult<IActionResult>(new OkObjectResult(value));
    }
}