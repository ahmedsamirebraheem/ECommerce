using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModule;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.Common_Result;
using ECommerce.Shared.OrderDtos;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service;

public class OrderService(IMapper mapper,IBasketRepository basketRepository,IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email)
    {
        var orderAddress = mapper.Map<OrderAddress>(orderDto.Address);
        var basket = await basketRepository.GetBasketAsync(orderDto.BasketId);
        if (basket == null)
        {
            return Error.NotFound("Basket Not found",$"Basket with id : {orderDto.BasketId} is not found");
        }
        List<OrderItem> OrderItems = [];
        foreach (var item in basket.Items)
        {
            var product = await unitOfWork.GerRepository<Product,int>().GetByIdAsync(item.Id);
            if (product == null)
            {
                return Error.NotFound("Product Not found", $"Product with id : {item.Id} is not found");
            }
            OrderItems.Add( CreateOrderItem(item, product));
        }
        var deliveryMethod = await unitOfWork.GerRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);
        if(deliveryMethod is null)
        {
            return Error.NotFound("delivery method not found",$"delivery method with id {orderDto.DeliveryMethodId} not found");
        }
        var subtotal = OrderItems.Sum(i => i.Price * i.Quantity);

        var order = new Order()
        {
            Address = orderAddress,
            DeliveryMethod = deliveryMethod,
            Item = OrderItems,
            Subtotal = subtotal,
            UserEmail = email
        };
        await unitOfWork.GerRepository<Order, Guid>().AddAsync(order);
        int result = await unitOfWork.SaveChangeAsync();

        if (result == 0) return Error.Failure();

        return mapper.Map<OrderToReturnDto>(order);
    }

    private static OrderItem CreateOrderItem(BasketItem item,Product product )
    {
        return new OrderItem()
        {
            Product = new ProductItemOrdered
            {
                ProductId = product.Id,
                ProductName = product.Name,
                PictureUrl = product.PictureUrl
            },
            Price = product.Price,
            Quantity = item.Quantity
        };
    }
}
