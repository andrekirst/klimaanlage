using System.Device.Gpio;
using Api.Proxies;

namespace Api.Facades;

public interface IGpioControllerFacade
{
    void WriteHigh(int pinNumber);
    void WriteLow(int pinNumber);
    bool IsHigh(int pinNumber);
    bool IsLow(int pinNumber);
    void OpenPin(int pinNumber);
    void OpenOutputPin(int pinNumber);
    void SetupFanPwm(int fanPwmPin);
    void ChangePwmValues(int pinNumber, int? frequency = null, double? dutyCycle = null);
    void StopPwm(int pinNumber);
}

public class GpioControllerFacade(IGpioControllerProxy gpioControllerProxy) : IGpioControllerFacade
{
    public void WriteHigh(int pinNumber) => gpioControllerProxy.Write(pinNumber, PinValue.High);
    public void WriteLow(int pinNumber) => gpioControllerProxy.Write(pinNumber, PinValue.Low);
    public bool IsHigh(int pinNumber) => gpioControllerProxy.Read(pinNumber) == PinValue.High;
    public bool IsLow(int pinNumber) => gpioControllerProxy.Read(pinNumber) == PinValue.Low;
    public void OpenPin(int pinNumber) => gpioControllerProxy.OpenPin(pinNumber);
    public void OpenOutputPin(int pinNumber) => gpioControllerProxy.OpenPin(pinNumber, PinMode.Output);
    public void SetupFanPwm(int pinNumber) => gpioControllerProxy.SetupPwm(pinNumber);
    public void ChangePwmValues(int pinNumber, int? frequency = null, double? dutyCycle = null) => gpioControllerProxy.ChangePwmValues(pinNumber, frequency, dutyCycle);
    public void StopPwm(int pinNumber) => gpioControllerProxy.StopPwm(pinNumber);
}