using System.Device.Gpio;
using System.Device.Pwm.Drivers;

namespace Api.Proxies;

public class GpioControllerProxy(
    GpioController gpioController,
    ILogger<GpioControllerProxy> logger) : IGpioControllerProxy, IDisposable
{
    private readonly Dictionary<int, SoftwarePwmChannel> _softwarePwmChannels = new Dictionary<int, SoftwarePwmChannel>();

    public void SetPinMode(int pinNumber, PinMode pinMode)
    {
        gpioController.SetPinMode(pinNumber, pinMode);
    }

    public PinMode GetPinMode(int pinNumber) => gpioController.GetPinMode(pinNumber);
    
    public void Write(int pinNumber, PinValue pinValue)
    {
        logger.LogInformation("Write pin {pinNumber} with value {pinValue}", pinNumber, pinValue);
        gpioController.Write(pinNumber, pinValue);
    }
    
    public PinValue Read(int pinNumber) => gpioController.Read(pinNumber);
    public void OpenPin(int pinNumber)
    {
        gpioController.OpenPin(pinNumber);
    }

    public void OpenPin(int pinNumber, PinMode pinMode)
    {
        logger.LogInformation("Open pin {pinNumber} with mode {pinMode}", pinNumber, pinMode);
        gpioController.OpenPin(pinNumber, pinMode);
    }

    public bool IsPinOpen(int pinNumber)
    {
        var isPinOpen = gpioController.IsPinOpen(pinNumber);

        logger.LogInformation("Pin {pinNumber} is {isPinOpen}", pinNumber, isPinOpen ? "open" : "closed");

        return isPinOpen;
    }

    public void SetupPwm(int pinNumber, int frequency = 400, double dutyCycle = 0.5)
    {
        _softwarePwmChannels[pinNumber] = new SoftwarePwmChannel(pinNumber, frequency, dutyCycle, true, gpioController);
    }

    public void ChangePwmValues(int pinNumber, int? frequency = null, double? dutyCycle = null)
    {
        var softwarePwmChannel = _softwarePwmChannels[pinNumber];
        if (frequency is not null)
        {
            softwarePwmChannel.Frequency = frequency.Value;
        }

        if (dutyCycle is not null)
        {
            softwarePwmChannel.DutyCycle = dutyCycle.Value;
        }
    }

    public void StopPwm(int pinNumber)
    {
        var softwarePwmChannel = _softwarePwmChannels[pinNumber];
        softwarePwmChannel.Stop();
        _softwarePwmChannels.Remove(pinNumber);
    }

    public virtual void Dispose() => gpioController.Dispose();
}