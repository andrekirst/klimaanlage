using System.Device.Gpio;
using Api.Domain;
using Api.Facades;

namespace Api.Sensors;

public class Ds18B20(IGpioControllerFacade gpioControllerFacade) : IDs18B20
{
    private void Validate(int pinNumber)
    {
        var mode = gpioControllerFacade.GetPinMode(pinNumber);
        if (mode != PinMode.InputPullUp)
        {
            throw new ArgumentException($"Pin {pinNumber} must be {nameof(PinMode.InputPullUp)}", nameof(mode));
        }
    }

    public Temperature? Read(int pinNumber)
    {
        gpioControllerFacade.SetPinModeOutput(pinNumber);
        gpioControllerFacade.WriteLow(pinNumber);
        Wait(0.48);
        gpioControllerFacade.SetPinModeInput(pinNumber);
        Wait(0.07);

        if (!gpioControllerFacade.IsLow(pinNumber)) return null;
        
        Wait(0.41);
        WriteByte(pinNumber, 0xCC);
        WriteByte(pinNumber, 0xBE);

        var data = new byte[9];
        for (var i = 0; i < 9; i++)
        {
            data[i] = ReadByte(pinNumber);
        }

        var temperature = data[0] | (data[1] << 8);
        return new Temperature(temperature / 0.16);
    }

    private byte ReadByte(int pinNumber)
    {
        byte data = 0;
        for (var i = 0; i < 8; i++)
        {
            gpioControllerFacade.SetPinModeOutput(pinNumber);
            gpioControllerFacade.WriteLow(pinNumber);
            Wait(0.001);
            gpioControllerFacade.SetPinModeInput(pinNumber);
            Wait(0.014);

            if (gpioControllerFacade.IsHigh(pinNumber))
            {
                data |= (byte)(1 << i);
            }

            Wait(0.045);
        }

        return data;
    }

    private static void Wait(double milliseconds) => Thread.Sleep(TimeSpan.FromMilliseconds(milliseconds));

    private void WriteByte(int pinNumber, byte data)
    {
        for (var i = 0; i < 8; i++)
        {
            gpioControllerFacade.SetPinModeOutput(pinNumber);
            gpioControllerFacade.WriteLow(pinNumber);
            Wait(0.001);

            if ((data & (1 << i)) != 0)
            {
                gpioControllerFacade.WriteHigh(pinNumber);
            }

            Wait(0.06);
            gpioControllerFacade.SetPinModeInput(pinNumber);
            Wait(0.06);
        }
    }
}