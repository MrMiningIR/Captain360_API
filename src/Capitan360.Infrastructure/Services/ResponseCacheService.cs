using System.Text.Json;
using Capitan360.Domain.IService.CacheResponse;
using StackExchange.Redis;

namespace Capitan360.Infrastructure.Services;

public class ResponseCacheService(IConnectionMultiplexer redis) : IResponseCacheService
{
    //private readonly IDatabase _database = redis.GetDatabase(1);

    //public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
    //{
    //    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    //    var serializedResponse = JsonSerializer.Serialize(response, options);

    //    await _database.StringSetAsync(cacheKey, serializedResponse, timeToLive);
    //}

    //public async Task<string?> GetCachedResponseAsync(string cacheKey)
    //{
    //    var cachedResponse = await _database.StringGetAsync(cacheKey);

    //    if (cachedResponse.IsNullOrEmpty) return null;

    //    return cachedResponse;
    //}

    //public async Task RemoveCacheByPattern(string pattern)
    //{
    //    var server = redis.GetServer(redis.GetEndPoints().First());
    //    var keys = server.Keys(database: 1, pattern: $"*{pattern}*").ToArray();

    //    if (keys.Length != 0)
    //    {
    //        await _database.KeyDeleteAsync(keys);
    //    }
    //}
    public Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetCachedResponseAsync(string cacheKey)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCacheByPattern(string pattern)
    {
        throw new NotImplementedException();
    }
}
