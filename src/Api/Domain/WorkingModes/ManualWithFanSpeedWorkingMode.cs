using Api.Services;

namespace Api.Domain.WorkingModes;

public class ManualWithFanSpeedWorkingMode(IFanControlService fanControlService) : IWorkingMode
{
    public const string IdentifierKey = "ManualWithFanSpeed";

    public Task<bool> Setup(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }

    public Task Do(CancellationToken cancellationToken = default)
    {
        if (!RotatesWithExpectedRotation())
        {
            fanControlService.ChangeInputFanRotationInPercent(Percent ?? Percent.Max);
        }

        return Task.CompletedTask;
    }

    private bool RotatesWithExpectedRotation()
    {
        var rotationInPercent = fanControlService.GetInputFanRotationInPercent();
        return Math.Abs(rotationInPercent - Percent) > 0.05;
    }

    public Task ChangeFanSpeed(Percent percent, CancellationToken cancellationToken = default)
    {
        Percent = percent;
        return Task.CompletedTask;
    }

    public Percent? Percent { get; set; }

    public string Identifier => IdentifierKey;
    public string DisplayName => "Manual with fan speed";
}