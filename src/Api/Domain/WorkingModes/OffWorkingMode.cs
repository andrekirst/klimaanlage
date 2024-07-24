namespace Api.Domain.WorkingModes;

public class OffWorkingMode : IWorkingMode
{
    public Task<bool> Setup(CancellationToken cancellationToken = default) => Task.FromResult(true);

    public Task Do(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public string Identifier => "Off";
    public string DisplayName => "Off";
}