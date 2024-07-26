using Api.Domain;

namespace Api.Services;

public interface ITemperatureReader
{
    Task<Temperature?> GetOutsideTemperature(bool forceUseSensor = false, CancellationToken cancellationToken = default);
}