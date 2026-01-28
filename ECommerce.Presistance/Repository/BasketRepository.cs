using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ECommerce.Presistance.Repository;

public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
{
    private readonly IDatabase _database = connection.GetDatabase();

    public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeToLive = default)
    {
        var jsonBasket = JsonSerializer.Serialize(basket);

        // حددنا المدة الافتراضية 7 أيام لو متبعتش حاجة
        var expiry = timeToLive == default ? TimeSpan.FromDays(7) : timeToLive;

        var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, expiry);

        if (isCreatedOrUpdated)
        {
            return await GetBasketAsync(basket.Id);
        }

        return null;
    }

    public async Task<bool> DeleteBasketAsync(string basketId)=> await _database.KeyDeleteAsync(basketId);

    public async Task<CustomerBasket?> GetBasketAsync(string basketId)
    {
        var data = await _database.StringGetAsync(basketId);
        if (data.IsNullOrEmpty)
            return null;
        // الحل هنا: حولناه لـ string صراحة عشان نمنع الـ Ambiguity

        return JsonSerializer.Deserialize<CustomerBasket>(data.ToString());
    }
}
