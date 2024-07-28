namespace Api.Services;

public interface ITemperatureWriter
{
    Task SetOutsideTemperatur(CancellationToken cancellationToken = default);
}

public class TemperatureWriter(
    ITemperatureStorage temperatureStorage,
    ITemperatureSensor temperatureSensor) : ITemperatureWriter
{
    public async Task SetOutsideTemperatur(CancellationToken cancellationToken = default)
    {
        var temperature = await temperatureSensor.ReadAsync(PinConfiguration.Temperature.Output.GpioPin, cancellationToken);
        if (temperature != null) 
        {
            await temperatureStorage.SetOutsideTemperature(temperature, cancellationToken); 
        }
    }
}