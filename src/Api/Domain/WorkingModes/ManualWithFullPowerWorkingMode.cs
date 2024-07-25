using Api.Services;

namespace Api.Domain.WorkingModes;

public class ManualWithFullPowerWorkingMode(
    IRelayControlService relayControlService,
    IFanControlService fanControlService) : IWorkingMode
{
    public Task<bool> Setup(CancellationToken cancellationToken = default)
    {
        relayControlService.TurnOutputRelayOn();
        return Task.FromResult(fanControlService.IsOutputFanOn());
    }

    public Task Do(CancellationToken cancellationToken = default)
    {
        if (fanControlService.IsOutputFanOff())
        {
            relayControlService.TurnOutputRelayOn();
        }

        return Task.CompletedTask;
    }

    public const string IdentifierKey = nameof(ManualWithFullPowerWorkingMode);
    public string Identifier => IdentifierKey;
    public string DisplayName => "Manual with full power";
}