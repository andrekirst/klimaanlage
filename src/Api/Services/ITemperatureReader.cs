using Api.Domain;

namespace Api.Services;

public interface ITemperatureReader
{
    Task<Temperature> GetInsideTemperature(CancellationToken cancellationToken = default);
    Task<Temperature> GetOutsideTemperature(CancellationToken cancellationToken = default);
}