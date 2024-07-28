using Api.Domain;

namespace Api.Services;

public interface ITemperatureSensor
{
    Task<Temperature?> ReadAsync(int pinNumber, CancellationToken cancellationToken = default);
}