using Api.Services;

namespace Api.HostedServices;

public class ReadTemperatureHostedService(
    ILogger<ReadTemperatureHostedService> logger,
    ITemperatureReader temperatureReader): BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}