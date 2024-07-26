using Api.Domain;
using Api.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Services;

public interface ITemperatureStorage
{
    Task SetOutsideTemperature(Temperature temperature, CancellationToken cancellationToken = default);
    Task<Temperature?> GetOutsideTemperature(CancellationToken cancellationToken = default);
}

public class InMemoryTemperatureStorage(IMemoryCache memoryCache) : ITemperatureStorage
{
    private const string OutsideTemperatureCacheKey = "InMemoryTemperatureStorage:Output";

    public Task SetOutsideTemperature(Temperature temperature, CancellationToken cancellationToken = default)
    {
        memoryCache.Set(OutsideTemperatureCacheKey, temperature.Value, 10.Seconds());
        return Task.CompletedTask;
    }

    public Task<Temperature?> GetOutsideTemperature(CancellationToken cancellationToken = default)
    {
        var exists = memoryCache.TryGetValue(OutsideTemperatureCacheKey, out double value);
        return Task.FromResult(exists ? new Temperature(value) : null);
    }
}