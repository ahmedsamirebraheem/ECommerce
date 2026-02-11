using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.OrderModule;

public class OrderItem : BaseEntity<int>
{
   public ProductItemOrdered Product { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

}
