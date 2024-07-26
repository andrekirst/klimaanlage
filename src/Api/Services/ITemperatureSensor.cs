using Api.Domain;

namespace Api.Services;

public interface ITemperatureSensor
{
    Task<Temperature?> Read(int pinNumber, CancellationToken cancellationToken = default);
}