using Api.Domain;
using Api.Sensors;

namespace Api.Services;

public class Ds18B20TemperatureSensor(IDs18B20 ds18B20) : ITemperatureSensor
{
    public Task<Temperature?> Read(int pinNumber, CancellationToken cancellationToken = default) => ds18B20.Read(pinNumber, cancellationToken);
}