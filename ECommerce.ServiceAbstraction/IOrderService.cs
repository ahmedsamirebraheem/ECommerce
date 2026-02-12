using ECommerce.Shared.Common_Result;
using ECommerce.Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.ServiceAbstraction;

public interface IOrderService
{
    //create order 
    Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email);   
}
