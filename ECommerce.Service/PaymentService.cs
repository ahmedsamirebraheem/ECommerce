using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.BasketDtos;
using ECommerce.Shared.Common_Result;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using Product = ECommerce.Domain.Entities.ProductModule.Product;

namespace ECommerce.Service;

public class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork,IConfiguration configuration,IMapper mapper) : IPaymentService
{
    public async Task<Result<BasketDTO>> CreatePaymentAsync(string basketId)
    {
        var basket = await basketRepository.GetBasketAsync(basketId);
        if (basket is null) return Error.NotFound();
        foreach (var item in basket.Items)
        {
            var product = await unitOfWork.GerRepository<Product, int>().GetByIdAsync(item.Id);
            if (product is null) return Error.NotFound();
            item.Price = product.Price;
        }
        var subtotal = basket.Items.Sum(i => i.Price * i.Quantity);
        if (!basket.DeliveryMethodId.HasValue) return Error.NotFound();
        var delivery = await unitOfWork.GerRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
        basket.ShippingCost = delivery.Price;
        var amount = subtotal + delivery.Price;

        StripeConfiguration.ApiKey = configuration["StripeOption:Secretkey"];

        PaymentIntentService paymentIntentService = new();

        PaymentIntent paymentIntent;

        if(basket.PaymentIntentId is null)
        {
            var option = new PaymentIntentCreateOptions()
            {
                Amount = (long)amount * 100,
                Currency = "usd",
                PaymentMethodTypes =
                [
                    "card"
                ]
            };
            paymentIntent = await paymentIntentService.CreateAsync(option);
        }
        else
        {
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = (long)amount * 100
            }; 
            paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);

        };
        basket.PaymentIntentId = paymentIntent.Id;
        basket.ClientSecret = paymentIntent.ClientSecret;
        basket =   await basketRepository.CreateOrUpdateBasketAsync(basket); 
        return mapper.Map<BasketDTO>(basket);

    }
}
