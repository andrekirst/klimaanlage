using System.Device.Gpio;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Api.RemoteGpio.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RemoteController(GpioController gpioController) : ControllerBase
    {
        [HttpPost("{pinNumber}/close-pin")]
        public IActionResult ClosePin(int pinNumber)
        {
            gpioController.ClosePin(pinNumber);
            return Ok();
        }

        [HttpGet("{pinNumber}/pin-mode")]
        [ProducesResponseType<PinMode>((int)HttpStatusCode.OK)]
        public IActionResult GetPinMode(int pinNumber)
        {
            return Ok(gpioController.GetPinMode(pinNumber));
        }

        [HttpGet("{pinNumber}/is-pin-mode-supported")]
        [ProducesResponseType<bool>((int)HttpStatusCode.OK)]
        public IActionResult IsPinModeSupported(int pinNumber, PinMode pinMode)
        {
            var isSupported = gpioController.IsPinModeSupported(pinNumber, pinMode);
            return Ok(isSupported);
        }

        [HttpGet("{pinNumber}/is-pin-open")]
        [ProducesResponseType<bool>((int)HttpStatusCode.OK)]
        public IActionResult IsPinOpen(int pinNumber)
        {
            var value = gpioController.IsPinOpen(pinNumber);
            return Ok(value);
        }

        [HttpPost("{pinNumber}/open-pin")]
        [ProducesResponseType<GpioPin>((int)HttpStatusCode.OK)]
        public IActionResult OpenPin(int pinNumber)
        {
            var value = gpioController.OpenPin(pinNumber);
            return Ok(value);
        }
    }
}
