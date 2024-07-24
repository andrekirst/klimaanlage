namespace Api.Domain.WorkingModes;

public class AutomaticWithTargetTemperatureWorkingMode : IWorkingMode
{
    public Task<bool> Setup(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Do(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public string Identifier => "AutomaticWithTargetTemperature";
    public string DisplayName => "Automatic with target temperature";
}