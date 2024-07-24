using Api.Domain;
using Api.Facades;

namespace Api.Services;

public class FanControlService(IGpioControllerFacade gpioControllerFacade) : IFanControlService
{
    public bool IsInputFanOn()
    {
        var value = gpioControllerFacade.IsHigh(PinConfiguration.Fan.Input.RelayGpioPin);

        return value;
    }

    public void ChangeInputFanRotationInPercent(Percent percent)
    {
        gpioControllerFacade.ChangePwmValues(PinConfiguration.Fan.Input.FanPwmPin, 400, 0.25);
    }

    public Percent GetInputFanRotationInPercent()
    {
        throw new NotImplementedException();
    }
}