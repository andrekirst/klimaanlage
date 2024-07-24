namespace Api.Domain.WorkingModes;

public class AutomaticWithTargetTemperatureAndFanSpeedWorkingMode : IWorkingMode
{
    public Task<bool> Setup(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Do(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public string Identifier => "AutomaticWithTargetTemperatureAndFanSpeed";
    public string DisplayName => "Automatic with target temperature and fan speed";
}