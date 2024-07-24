namespace Api.Domain;

public class Percent
{
    public double Value { get; }

    public Percent(double value)
    {
        Validate(value);
        Value = value;
    }

    private static void Validate(double value)
    {
        if (value is < MinValue or > MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, $"Min: {MinValue:F2}, Max: {MaxValue:F2}, Current: {value:F2}");
        }
    }

    private const double MinValue = 0.0;
    private const double MaxValue = 1.0;
    public static Percent Min => new Percent(MinValue);
    public static Percent Max => new Percent(MaxValue);
    public static Percent Of(double value) => new Percent(value);

    // TODO Range checking
    public static double operator -(Percent percent, double value) => percent.Value - value;
    public static double operator -(double value, Percent? percent) => value - (percent?.Value ?? 0);
    public static double operator -(Percent? left, Percent? right) => (left?.Value ?? MinValue) - (right?.Value ?? MinValue);
}