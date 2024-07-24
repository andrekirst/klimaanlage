using Api.Domain.WorkingModes;

namespace Api.HostedServices;

public class RunWorkingModeHostedService : BackgroundService
{
    private readonly CurrentWorkingModeSelector _currentWorkingModeSelector;

    public RunWorkingModeHostedService(CurrentWorkingModeSelector currentWorkingModeSelector)
    {
        _currentWorkingModeSelector = currentWorkingModeSelector;
    }

    private IWorkingMode? _currentWorkingMode;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _currentWorkingMode = await _currentWorkingModeSelector.Get();
        await _currentWorkingMode.Setup(cancellationToken);

        var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(10));
        do
        {
            await DetermineNewMethod(cancellationToken);

            await _currentWorkingMode.Do(cancellationToken);
        } while (await periodicTimer.WaitForNextTickAsync(cancellationToken));
    }

    private async Task DetermineNewMethod(CancellationToken cancellationToken)
    {
        var workingMode = await _currentWorkingModeSelector.Get();
        if (workingMode != _currentWorkingMode)
        {
            await workingMode.Setup(cancellationToken);
            _currentWorkingMode = workingMode;
        }
    }
}