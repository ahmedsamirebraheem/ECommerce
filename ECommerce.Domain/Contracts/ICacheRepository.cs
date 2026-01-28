using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts;

public interface ICacheRepository
{
    Task<string?> GetAsync(string cacheKey);
    Task SetAsync(string cacheKey,string cacheValue,TimeSpan timeToLive);
}
