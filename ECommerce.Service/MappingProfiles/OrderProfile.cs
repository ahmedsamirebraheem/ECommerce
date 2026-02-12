using ECommerce.Domain.Entities.OrderModule;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.OrderDtos;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service.MappingProfiles;

public class OrderProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // 1. Mapping للـ Order الرئيسي
        config.NewConfig<Order, OrderToReturnDto>()
            .Map(dest => dest.DeliveryMethod, src => src.DeliveryMethod.ShortName)
            .Map(dest => dest.Total, src => src.GetTotal())
            .Map(dest => dest.OrderItems, src => src.Item);

        // 2. Mapping للـ OrderItem مع حل مشكلة الـ PictureUrl
        config.NewConfig<OrderItem, OrderItemDto>()
            .Map(dest => dest.ProductName, src => src.Product.ProductName)
            // نستخدم الخدمة المسجلة في الـ DI لحل الرابط
            .Map(dest => dest.PictureUrl, src =>
                MapContext.Current != null
                ? MapContext.Current.GetService<IPictureUrlResolver>().Resolve(src.Product.PictureUrl)
                : src.Product.PictureUrl);

        config.NewConfig<OrderAddress, AddressDto>().TwoWays();
    }
}
