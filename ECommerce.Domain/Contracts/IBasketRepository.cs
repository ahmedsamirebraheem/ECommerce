using ECommerce.Domain.Entities.BasketModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(string basketId);
    Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket,TimeSpan timeToLive=default);
    Task<bool> DeleteBasketAsync(string basketId);

}
