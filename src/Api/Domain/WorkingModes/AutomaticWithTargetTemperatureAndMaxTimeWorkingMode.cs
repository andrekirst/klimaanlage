namespace Api.Domain.WorkingModes;

public class AutomaticWithTargetTemperatureAndMaxTimeWorkingMode : IWorkingMode
{
    public Task<bool> Setup(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Do(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public string Identifier => "AutomaticWithTargetTemperatureAndMaxTime";
    public string DisplayName => "Automatic with target temperature and max time";
}