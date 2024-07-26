using System.Device.Gpio;
using Api.Domain;
using Api.Extensions;
using Api.Facades;

namespace Api.Sensors;

public class Ds18B20(IGpioControllerFacade gpioControllerFacade) : Sensor, IDs18B20
{
    public async Task<Temperature?> Read(int pinNumber, CancellationToken cancellationToken = default)
    {
        Validate(pinNumber);

        gpioControllerFacade.SetPinModeOutput(pinNumber);
        gpioControllerFacade.WriteLow(pinNumber);
        await WaitAsync(0.48.Seconds(), cancellationToken);
        gpioControllerFacade.SetPinModeInput(pinNumber);
        await WaitAsync(0.07.Seconds(), cancellationToken);

        if (!gpioControllerFacade.IsLow(pinNumber)) return null;
        
        await WaitAsync(0.41.Seconds(), cancellationToken);
        await WriteByteAsync(pinNumber, 0xCC, cancellationToken);
        await WriteByteAsync(pinNumber, 0xBE, cancellationToken);

        var data = new byte[9];
        for (var i = 0; i < 9; i++)
        {
            data[i] = await ReadByteAsync(pinNumber, cancellationToken);
        }

        var temperature = data[0] | (data[1] << 8);
        return new Temperature(temperature / 0.16);
    }

    private async ValueTask<byte> ReadByteAsync(int pinNumber, CancellationToken cancellationToken = default)
    {
        byte data = 0;
        for (var i = 0; i < 8; i++)
        {
            gpioControllerFacade.SetPinModeOutput(pinNumber);
            gpioControllerFacade.WriteLow(pinNumber);
            await WaitAsync(0.001.Seconds(), cancellationToken);
            gpioControllerFacade.SetPinModeInput(pinNumber);
            await WaitAsync(0.014.Seconds(), cancellationToken);

            if (gpioControllerFacade.IsHigh(pinNumber))
            {
                data |= (byte)(1 << i);
            }

            await WaitAsync(0.045.Seconds(), cancellationToken);
        }

        return data;
    }

    private async Task WriteByteAsync(int pinNumber, byte data, CancellationToken cancellationToken = default)
    {
        for (var i = 0; i < 8; i++)
        {
            gpioControllerFacade.SetPinModeOutput(pinNumber);
            gpioControllerFacade.WriteLow(pinNumber);
            await WaitAsync(0.001.Seconds(), cancellationToken);

            if ((data & (1 << i)) != 0)
            {
                gpioControllerFacade.WriteHigh(pinNumber);
            }

            await WaitAsync(0.06.Seconds(), cancellationToken);
            gpioControllerFacade.SetPinModeInput(pinNumber);
            await WaitAsync(0.06.Seconds(), cancellationToken);
        }
    }

    private void Validate(int pinNumber)
    {
        var mode = gpioControllerFacade.GetPinMode(pinNumber);
        if (mode != PinMode.InputPullUp)
        {
            throw new ArgumentException($"Pin {pinNumber} must be {nameof(PinMode.InputPullUp)}", nameof(mode));
        }
    }
}