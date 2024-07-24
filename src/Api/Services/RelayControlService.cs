using Api.Facades;

namespace Api.Services;

public interface IRelayControlService
{
    void TurnInputRelayOff();
    void TurnInputRelayOn();
}

public class RelayControlService(IGpioControllerFacade gpioControllerFacade) : IRelayControlService
{
    public void TurnInputRelayOff() => gpioControllerFacade.WriteLow(PinConfiguration.Fan.Input.RelayGpioPin);

    public void TurnInputRelayOn() => gpioControllerFacade.WriteHigh(PinConfiguration.Fan.Input.RelayGpioPin);
}