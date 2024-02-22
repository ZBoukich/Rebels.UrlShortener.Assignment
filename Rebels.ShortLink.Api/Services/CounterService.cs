using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics.Metrics;

namespace Rebels.ShortLink.Api.Services
{
    public class CounterService : ICounterService
    {
        const string Counter = "Counter";
        private readonly IMemoryCache _memoryCache;

        public CounterService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public long GetCounter()
        {
            var uniqueId = _memoryCache.Get(Counter) ?? throw new NullReferenceException("No counter found in the memorycache.");
            return (long)uniqueId;
        }

        public void UpdateCounter()
        {
            _memoryCache.Set(Counter, GetCounter() + 1);
        }
    }
}
