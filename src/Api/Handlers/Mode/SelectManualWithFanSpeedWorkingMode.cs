using Api.Domain;
using Api.Domain.WorkingModes;

namespace Api.Handlers.Mode;

public class SelectManualWithFanSpeedWorkingModeRequest : IRequest<bool>
{
    public double Percent { get; set; }
}

public class SelectManualWithFanSpeedWorkingModeRequestHandler(
    CurrentWorkingModeSelector currentWorkingModeSelector,
    IEnumerable<IWorkingMode> workingModes) : IRequestHandler<SelectManualWithFanSpeedWorkingModeRequest, bool>
{
    public async ValueTask<bool> Handle(SelectManualWithFanSpeedWorkingModeRequest request, CancellationToken cancellationToken)
    {
        var selectedworkingMode = workingModes.Single(w => w.Identifier == ManualWithFanSpeedWorkingMode.IdentifierKey);

        var successful = await selectedworkingMode.Setup(cancellationToken);
        if (!successful) return false;

        var manualWithFanSpeedWorkingMode = selectedworkingMode as ManualWithFanSpeedWorkingMode;

        manualWithFanSpeedWorkingMode?.ChangeFanSpeed(Percent.Of(request.Percent), cancellationToken);

        await currentWorkingModeSelector.Set(selectedworkingMode.Identifier);
        return true;
    }
}