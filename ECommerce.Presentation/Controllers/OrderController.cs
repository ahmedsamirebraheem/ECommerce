using ECommerce.ServiceAbstraction;
using ECommerce.Shared.OrderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Presentation.Controllers;


public class OrderController(IOrderService orderService) : ApiBaseController
{
    // Post: BaseUrl/api/Order
    [HttpPost("/api/Order")]
    [Authorize]
    public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var order = await orderService.CreateOrderAsync(orderDto, email!);
        return HandleResult(order);
    }
}
