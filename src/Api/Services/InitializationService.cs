using Api.Facades;

namespace Api.Services;

public class InitializationService(IGpioControllerFacade gpioControllerFacade) : IInitializationService
{
    public void Initialize()
    {
        gpioControllerFacade.OpenOutputPin(PinConfiguration.Fan.Input.RelayGpioPin);
        gpioControllerFacade.OpenOutputPin(PinConfiguration.Fan.Input.FanPwmPin);
        gpioControllerFacade.SetupFanPwm(PinConfiguration.Fan.Input.FanPwmPin);
    }
}