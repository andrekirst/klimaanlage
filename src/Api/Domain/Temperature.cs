namespace Api.Domain;

public class Temperature
{
    public double Value { get; }

    public Temperature(double value)
    {
        Value = value;
    }
}