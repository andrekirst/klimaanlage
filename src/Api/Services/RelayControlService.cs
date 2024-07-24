using Api.Facades;

namespace Api.Services;

public interface IRelayControlService
{
    void TurnInputRelayOff();
    void TurnInputRelayOn();
    void TurnOutputRelayOn();
    void TurnOutputRelayOff();
}

public class RelayControlService(IGpioControllerFacade gpioControllerFacade) : IRelayControlService
{
    public void TurnInputRelayOff() => gpioControllerFacade.WriteLow(PinConfiguration.Fan.Input.RelayGpioPin);
    public void TurnInputRelayOn() => gpioControllerFacade.WriteHigh(PinConfiguration.Fan.Input.RelayGpioPin);
    public void TurnOutputRelayOn() => gpioControllerFacade.WriteHigh(PinConfiguration.Fan.Output.RelayGpioPin);
    public void TurnOutputRelayOff() => gpioControllerFacade.WriteLow(PinConfiguration.Fan.Output.RelayGpioPin);
}