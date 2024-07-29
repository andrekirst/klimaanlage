using System.Drawing;
using Api.Facades;
using Api.Hardware.Displays;
using Iot.Device.Graphics;
using Iot.Device.Ssd13xx.Commands;
using Iot.Device.Ssd13xx.Commands.Ssd1306Commands;

namespace Api.Services;

public class InitializationService(
    IGpioControllerFacade gpioControllerFacade,
    Ssd1306Size128X64 display) : IInitializationService
{
    public void Initialize()
    {
        display.EnableDisplay(true);

        using (var image = BitmapImage.CreateBitmap(128, 64, PixelFormat.Format32bppArgb))
        {
            image.Clear(Color.Black);
            var g = image.GetDrawingApi();
        }

        gpioControllerFacade.OpenOutputPin(PinConfiguration.Fan.Input.RelayGpioPin);
        gpioControllerFacade.OpenOutputPin(PinConfiguration.Fan.Input.FanPwmPin);
        gpioControllerFacade.SetupFanPwm(PinConfiguration.Fan.Input.FanPwmPin);

        gpioControllerFacade.OpenOutputPin(PinConfiguration.Fan.Output.RelayGpioPin);
        gpioControllerFacade.OpenOutputPin(PinConfiguration.Fan.Output.FanPwmPin);
        gpioControllerFacade.SetupFanPwm(PinConfiguration.Fan.Output.FanPwmPin);
    }
}