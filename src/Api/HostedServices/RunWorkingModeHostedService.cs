using Api.Domain.WorkingModes;

namespace Api.HostedServices;

public class RunWorkingModeHostedService : BackgroundService
{
    private readonly CurrentWorkingModeSelector _currentWorkingModeSelector;
    private readonly ILogger<RunWorkingModeHostedService> _logger;

    public RunWorkingModeHostedService(
        CurrentWorkingModeSelector currentWorkingModeSelector,
        ILogger<RunWorkingModeHostedService> logger)
    {
        _currentWorkingModeSelector = currentWorkingModeSelector;
        _logger = logger;
    }

    private IWorkingMode? _currentWorkingMode;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _currentWorkingMode = await _currentWorkingModeSelector.Get();

        _logger.LogInformation("Current word mode: {currentWorkMode}", _currentWorkingMode.DisplayName);

        _logger.LogInformation("start {currentWorkMode}:{setup}", _currentWorkingMode.Identifier, nameof(IWorkingMode.Setup));

        await _currentWorkingMode.Setup(cancellationToken);

        _logger.LogInformation("finished {currentWorkMode}:{setup}", _currentWorkingMode.Identifier, nameof(IWorkingMode.Setup));

        var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(10));
        do
        {
            await DetermineWorkingMode(cancellationToken);

            await _currentWorkingMode.Do(cancellationToken);
        } while (await periodicTimer.WaitForNextTickAsync(cancellationToken));
    }

    private async Task DetermineWorkingMode(CancellationToken cancellationToken)
    {
        var workingMode = await _currentWorkingModeSelector.Get();
        _logger.LogInformation("Determine new working mode: {workingMode}", workingMode.Identifier);

        if (workingMode != _currentWorkingMode)
        {
            _logger.LogInformation("start new {currentWorkMode}:{setup}", workingMode.Identifier, nameof(IWorkingMode.Setup));
            await workingMode.Setup(cancellationToken);
            _logger.LogInformation("finished new {currentWorkMode}:{setup}", workingMode.Identifier, nameof(IWorkingMode.Setup));
            _currentWorkingMode = workingMode;
        }
    }
}