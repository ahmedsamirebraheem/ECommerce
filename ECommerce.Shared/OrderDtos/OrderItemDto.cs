using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.OrderDtos;

public class OrderItemDto
{
    public string ProductName { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
