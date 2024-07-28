namespace Api;

public static class PinConfiguration
{
    public static class Fan
    {
        public static class Output
        {
            public const int RelayGpioPin = 20;
            public const int FanPwmPin = 12;
        }

        public static class Input
        {
            public const int RelayGpioPin = 16;
            public const int FanPwmPin = 13;
        }
    }

    public static class Temperature
    {
        public static class Output
        {
            public static int GpioPin = 4;
        }

        public static class Inside
        {
            public static int GpioPin = 5;
        }
    }
}