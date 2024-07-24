using System.Device.Gpio;

namespace Api.Proxies;

public interface IGpioControllerProxy
{
    void SetPinMode(int pinNumber, PinMode pinMode);
    PinMode GetPinMode(int pinNumber);
    void Write(int pinNumber, PinValue pinValue);
    PinValue Read(int pinNumber);
    void OpenPin(int pinNumber);
    void OpenPin(int pinNumber, PinMode pinMode);
    bool IsPinOpen(int pinNumber);
    void SetupPwm(int pinNumber, int frequency = 400, double dutyCycle = 0.5);
    void ChangePwmValues(int pinNumber, int? frequency = null, double? dutyCycle = null);
    void StopPwm(int pinNumber);
}