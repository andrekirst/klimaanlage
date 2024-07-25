using System.Device.Gpio;
using Api.Domain;
using Iot.Device.OneWire;

namespace Api.Services;

public class TemperatureReader(GpioController gpioController) : ITemperatureReader
{
    public async Task<Temperature> GetInsideTemperature(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Temperature> GetOutsideTemperature(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}