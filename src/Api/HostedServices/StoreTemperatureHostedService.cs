using Api.Extensions;
using Api.Services;

namespace Api.HostedServices;

public class StoreTemperatureHostedService(
    ILogger<StoreTemperatureHostedService> logger,
    ITemperatureWriter temperatureWriter): BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var periodicTimer = new PeriodicTimer(5.Seconds());

        while (!cancellationToken.IsCancellationRequested &&
               await periodicTimer.WaitForNextTickAsync(cancellationToken))
        {
            await temperatureWriter.SetOutsideTemperatur(cancellationToken);
        }
    }
}