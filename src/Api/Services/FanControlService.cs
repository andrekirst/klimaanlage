using Api.Domain;
using Api.Facades;

namespace Api.Services;

public class FanControlService(IGpioControllerFacade gpioControllerFacade) : IFanControlService
{
    public bool IsInputFanOn() => gpioControllerFacade.IsHigh(PinConfiguration.Fan.Input.RelayGpioPin);
    public bool IsInputFanOff() => gpioControllerFacade.IsLow(PinConfiguration.Fan.Input.RelayGpioPin);

    public void ChangeInputFanRotationInPercent(Percent percent)
    {
        gpioControllerFacade.ChangePwmValues(PinConfiguration.Fan.Input.FanPwmPin, 400, 0.25);
    }

    public Percent GetInputFanRotationInPercent()
    {
        throw new NotImplementedException();
    }

    public bool IsOutputFanOn() => gpioControllerFacade.IsHigh(PinConfiguration.Fan.Output.RelayGpioPin);
    public bool IsOutputFanOff() => gpioControllerFacade.IsLow(PinConfiguration.Fan.Output.RelayGpioPin);

    public void ChangeOutputFanRotationInPercent(Percent percent)
    {
        gpioControllerFacade.ChangePwmValues(PinConfiguration.Fan.Output.FanPwmPin, 400, 0.25);
    }
}