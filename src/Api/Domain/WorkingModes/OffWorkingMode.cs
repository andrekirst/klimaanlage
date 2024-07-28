using Api.Services;

namespace Api.Domain.WorkingModes;

public class OffWorkingMode(IRelayControlService relayControlService) : IWorkingMode
{
    public async Task<bool> Setup(CancellationToken cancellationToken = default)
    {
        relayControlService.TurnOutputRelayOff();

        await Task.Delay(50, cancellationToken);

        relayControlService.TurnInputRelayOff();
        return true;
    }

    public Task Do(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public const string IdentifierKey = nameof(OffWorkingMode);
    public string Identifier => IdentifierKey;
    public string DisplayName => "Off";
}