using Api.Domain.WorkingModes;

namespace Api.Handlers.Mode;

public class SelectOffWorkingModeRequest : IRequest<bool>;

public class SelectOffWorkingModeRequestHandler(
    CurrentWorkingModeSelector currentWorkingModeSelector,
    IEnumerable<IWorkingMode> workingModes) : IRequestHandler<SelectOffWorkingModeRequest, bool>
{
    public async ValueTask<bool> Handle(SelectOffWorkingModeRequest request, CancellationToken cancellationToken)
    {
        var selectedWorkingMode = workingModes.Single(w => w.Identifier == OffWorkingMode.IdentifierKey);

        var successful = await selectedWorkingMode.Setup(cancellationToken);
        if (!successful) return false;

        await currentWorkingModeSelector.Set(selectedWorkingMode.Identifier);
        return true;
    }
}