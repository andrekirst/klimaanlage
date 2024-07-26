namespace Api.Sensors;

public abstract class Sensor
{
    protected virtual void Wait(double milliseconds) => Thread.Sleep(TimeSpan.FromMilliseconds(milliseconds));
    protected virtual Task WaitAsync(TimeSpan delay, CancellationToken cancellationToken = default) => Task.Delay(delay, cancellationToken);
}