using System.Device.I2c;
using Iot.Device.Ssd13xx;

namespace Api.Hardware.Displays;

public class Ssd1306Size128X64(I2cDevice i2CDevice) : Ssd1306(i2CDevice, 128, 64);