using Api.Services;

namespace Api.Domain.WorkingModes;

public class ManualWithFullPowerWorkingMode(
    IRelayControlService relayControlService,
    IFanControlService fanControlService) : IWorkingMode
{
    public Task<bool> Setup(CancellationToken cancellationToken = default)
    {
        if (fanControlService.IsOutputFanOff())
        {
            relayControlService.TurnOutputRelayOn();
        }

        if (fanControlService.IsInputFanOn())
        {
            relayControlService.TurnInputRelayOn();
        }

        return Task.FromResult(fanControlService.IsOutputFanOn() && fanControlService.IsInputFanOn());
    }

    public Task Do(CancellationToken cancellationToken = default)
    {
        if (fanControlService.IsOutputFanOff())
        {
            relayControlService.TurnOutputRelayOn();
        }

        if (fanControlService.IsInputFanOff())
        {
            relayControlService.TurnInputRelayOn();
        }

        return Task.CompletedTask;
    }

    public const string IdentifierKey = nameof(ManualWithFullPowerWorkingMode);
    public string Identifier => IdentifierKey;
    public string DisplayName => "Manual with full power";
}