using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.OrderDtos;

public class OrderToReturnDto
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public ICollection<OrderItemDto> OrderItems { get; set; } = [];
    public AddressDto Address { get; set; } = null!;
    public string DeliveryMethod { get; set; } = string.Empty;
    public string OrderStatus { get; set; } = string.Empty;
    public DateTimeOffset OrderDate { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
}
