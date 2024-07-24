using System.Device.Gpio;

namespace Api.Proxies;

public class GpioControllerProxyUsingFakeGpioController(ILogger<GpioControllerProxyUsingFakeGpioController> logger) : IGpioControllerProxy
{
    public void SetPinMode(int pinNumber, PinMode pinMode)
    {
    }

    public PinMode GetPinMode(int pinNumber)
    {
        return PinMode.Output;
    }

    public void Write(int pinNumber, PinValue pinValue)
    {
    }

    public PinValue Read(int pinNumber)
    {
        return PinValue.High;
    }

    public void OpenPin(int pinNumber)
    {
    }

    public void OpenPin(int pinNumber, PinMode pinMode)
    {
    }

    public bool IsPinOpen(int pinNumber)
    {
        return true;
    }

    public void SetupPwm(int pinNumber, int frequency = 400, double dutyCycle = 0.5)
    {
    }

    public void ChangePwmValues(int pinNumber, int? frequency = null, double? dutyCycle = null)
    {
    }

    public void StopPwm(int pinNumber)
    {
    }
}