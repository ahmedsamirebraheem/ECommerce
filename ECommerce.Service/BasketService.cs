using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModule;
using ECommerce.Service.Exceptions;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.BasketDtos;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service;

public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
{
    public async Task<BasketDTO?> CreateOrUpdateBasketAsync(BasketDTO basketDto)
    {
        // 1. تحويل من DTO إلى Entity (للتعامل مع الداتابيز/ريديس)
        var customerBasket = mapper.Map<CustomerBasket>(basketDto);

        var createdOrUpdated = await basketRepository.CreateOrUpdateBasketAsync(customerBasket);

        // 2. لو الـ Repo رجع null، نرجع null. لو رجع داتا، نعمل مابينج
        return createdOrUpdated is null ? null : mapper.Map<BasketDTO>(createdOrUpdated);
    }

    public async Task<BasketDTO?> GetBasketAsync(string id)
    {
        var basket = await basketRepository.GetBasketAsync(id) ?? throw new BasketNotFoundException(id);

        return basket is null ? null : mapper.Map<BasketDTO>(basket);
    }

    public async Task<bool> DeleteBasketAsync(string id) => await basketRepository.DeleteBasketAsync(id);
}
