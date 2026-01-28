using ECommerce.Domain.Contracts;
using ECommerce.ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ECommerce.Service;

public class CacheService(ICacheRepository cacheRepository) : ICacheService
{
    public async Task<string?> GetAsync(string key)
    {
        return await cacheRepository.GetAsync(key);
    }

    public async Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive)
    {
        var value = JsonSerializer.Serialize(cacheValue);
        await cacheRepository.SetAsync(cacheKey, value, timeToLive);
    }
}
