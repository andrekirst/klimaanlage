namespace Api.Extensions;

public static class TimeExtensions
{
    public static TimeSpan Seconds(this double value) => TimeSpan.FromSeconds(value);
    public static TimeSpan Seconds(this int value) => TimeSpan.FromSeconds(value);
    public static TimeSpan Milliseconds(this double value) => TimeSpan.FromMilliseconds(value);
    public static TimeSpan Milliseconds(this int value) => TimeSpan.FromMilliseconds(value);
}