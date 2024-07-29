using System.Drawing;
using Api.Facades;
using Api.Hardware.Displays;
using Iot.Device.Graphics;
using Iot.Device.Graphics.SkiaSharpAdapter;
using Iot.Device.Ssd13xx.Commands;
namespace Api.Services;

public class InitializationService(IGpioControllerFacade gpioControllerFacade) : IInitializationService
{
    public void Initialize()
    {
        gpioControllerFacade.OpenOutputPin(PinConfiguration.Fan.Input.RelayGpioPin);
        gpioControllerFacade.OpenOutputPin(PinConfiguration.Fan.Input.FanPwmPin);
        gpioControllerFacade.SetupFanPwm(PinConfiguration.Fan.Input.FanPwmPin);

        gpioControllerFacade.OpenOutputPin(PinConfiguration.Fan.Output.RelayGpioPin);
        gpioControllerFacade.OpenOutputPin(PinConfiguration.Fan.Output.FanPwmPin);
        gpioControllerFacade.SetupFanPwm(PinConfiguration.Fan.Output.FanPwmPin);
    }
}