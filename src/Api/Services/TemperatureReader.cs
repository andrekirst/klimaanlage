using Api.Domain;

namespace Api.Services;

public class TemperatureReader(
    ITemperatureStorage temperatureStorage,
    ITemperatureSensor temperatureSensor
    ) : ITemperatureReader
{
    public async Task<Temperature?> GetOutsideTemperature(bool forceUseSensor = false, CancellationToken cancellationToken = default)
    {
        if (forceUseSensor)
        {
            var temperature = await temperatureSensor.Read(PinConfiguration.Temperature.Output.GpioPin, cancellationToken);
            if (temperature != null)
            {
                await temperatureStorage.SetOutsideTemperature(temperature, cancellationToken);
                return temperature;
            }
        }

        var storedTemperature = await temperatureStorage.GetOutsideTemperature(cancellationToken);
        if (storedTemperature != null)
        {
            return storedTemperature;
        }

        var newTemperature = await temperatureSensor.Read(PinConfiguration.Temperature.Output.GpioPin, cancellationToken);
        if (newTemperature == null) return null;
        
        await temperatureStorage.SetOutsideTemperature(newTemperature, cancellationToken);
        return newTemperature;
    }
}