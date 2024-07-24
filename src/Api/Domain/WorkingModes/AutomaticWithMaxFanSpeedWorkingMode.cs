namespace Api.Domain.WorkingModes;

public class AutomaticWithMaxFanSpeedWorkingMode : IWorkingMode
{
    public Task<bool> Setup(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Do(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public string Identifier => "AutomaticWithMaxFanSpeed";
    public string DisplayName => "Automatic with max fan speed";
}