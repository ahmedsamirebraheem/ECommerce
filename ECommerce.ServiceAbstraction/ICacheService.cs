using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.ServiceAbstraction;

public interface ICacheService
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string cacheKey,object cacheValue,TimeSpan timeToLive);
}
