using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace UserServer.DataAccess.Extensions
{
    public static class CacheExtensions
    {
        public static async Task<T> GetOrSerAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> valueFactory, TimeSpan? timeSpan = null)
        {
            var cachedValue = await cache.GetStringAsync(key);

            if (!string.IsNullOrEmpty(cachedValue))
            {
                var result = JsonSerializer.Deserialize<T>(cachedValue);

                if(result is not null)
                    return result;
            }

            var newValue = await valueFactory.Invoke();
            await cache.SetStringAsync(key, JsonSerializer.Serialize(newValue), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeSpan ?? TimeSpan.FromMinutes(60)
            });

            return newValue;
        }
    }
}
