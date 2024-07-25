using Microsoft.Extensions.Caching.Memory;

namespace Api.Domain.WorkingModes;

public class CurrentWorkingModeSelector(
    IMemoryCache memoryCache,
    IEnumerable<IWorkingMode> workingModes)
{
    public const string CacheKey = "CurrentWorkingModeSelector:Value";

    public Task<IWorkingMode> Get()
    {
        var value = memoryCache.Get<string>(CacheKey);

        var workingMode = value != null
            ? workingModes.Single(mode => mode.Identifier == value)
            : workingModes.Single(mode => mode.Identifier == OffWorkingMode.IdentifierKey);

        return Task.FromResult(workingMode);
    }

    public Task Set(string modeIdentifier)
    {
        memoryCache.Set(CacheKey, modeIdentifier, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365)
        });

        return Task.CompletedTask;
    }
}