using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.OrderModule;

public class ProductItemOrdered
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
}
