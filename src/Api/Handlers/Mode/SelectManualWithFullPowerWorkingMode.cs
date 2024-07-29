using Api.Domain.WorkingModes;
using Api.Hardware.Displays;

namespace Api.Handlers.Mode;

public class SelectManualWithFullPowerWorkingModeRequest : IRequest<bool>;

public class SelectManualWithFullPowerWorkingModeRequestHandler(
    CurrentWorkingModeSelector currentWorkingModeSelector,
    IEnumerable<IWorkingMode> workingModes,
    Ssd1306Size128X64 display) : IRequestHandler<SelectManualWithFullPowerWorkingModeRequest, bool>
{
    public async ValueTask<bool> Handle(SelectManualWithFullPowerWorkingModeRequest request, CancellationToken cancellationToken)
    {
        var selectedworkingMode = workingModes.Single(w => w.Identifier == ManualWithFullPowerWorkingMode.IdentifierKey);

        var successful = await selectedworkingMode.Setup(cancellationToken);
        if (!successful) return false;

        await currentWorkingModeSelector.Set(selectedworkingMode.Identifier);
        return true;
    }
}