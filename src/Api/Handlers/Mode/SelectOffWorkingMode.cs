using Api.Domain.WorkingModes;

namespace Api.Handlers.Mode;

public class SelectOffWorkingModeRequest : IRequest<bool>;

public class SelectOffWorkingModeRequestHandler(
    CurrentWorkingModeSelector currentWorkingModeSelector,
    IEnumerable<IWorkingMode> workingModes) : IRequestHandler<SelectOffWorkingModeRequest, bool>
{
    public async ValueTask<bool> Handle(SelectOffWorkingModeRequest request, CancellationToken cancellationToken)
    {
        var selectedworkingMode = workingModes.Single(w => w.Identifier == OffWorkingMode.IdentifierKey);

        var successful = await selectedworkingMode.Setup(cancellationToken);
        if (!successful) return false;

        await currentWorkingModeSelector.Set(selectedworkingMode.Identifier);
        return true;
    }
}