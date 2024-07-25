namespace Api.Domain.WorkingModes;

public class OffWorkingMode(ILogger<OffWorkingMode> logger) : IWorkingMode
{
    public Task<bool> Setup(CancellationToken cancellationToken = default) => Task.FromResult(true);

    public Task Do(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Off working mode executed");
        return Task.CompletedTask;
    }

    public const string IdentifierKey = nameof(OffWorkingMode);
    public string Identifier => IdentifierKey;
    public string DisplayName => "Off";
}