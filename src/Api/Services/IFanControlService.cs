using Api.Domain;

namespace Api.Services;

public interface IFanControlService
{
    bool IsInputFanOn();
    void ChangeInputFanRotationInPercent(Percent percent);
    Percent GetInputFanRotationInPercent();
}
