using ECommerce.Shared.BasketDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.ServiceAbstraction;

public interface IBasketService
{
    Task<BasketDTO?> GetBasketAsync(string id);
    Task<BasketDTO?> CreateOrUpdateBasketAsync(BasketDTO basket);
    Task<bool> DeleteBasketAsync(string id);

}
