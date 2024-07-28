using Api.Domain;

namespace Api.Services;

public interface IFanControlService
{
    bool IsInputFanOn();
    bool IsInputFanOff();
    void ChangeInputFanRotationInPercent(Percent percent);
    Percent GetInputFanRotationInPercent();
    bool IsOutputFanOn();
    bool IsOutputFanOff();
    void ChangeOutputFanRotationInPercent(Percent percent);
}
