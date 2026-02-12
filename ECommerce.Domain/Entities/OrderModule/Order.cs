using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.OrderModule;

public class Order : BaseEntity<Guid>
{
    public string UserEmail { get; set; } = null!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public OrderAddress Address { get; set; } = null!;
    public DeliveryMethod DeliveryMethod { get; set; } = null!;
    public int DeliveryMethodId { get; set; }
    public ICollection<OrderItem> Item { get; set; } = [];
    public decimal Subtotal { get; set; }

    public decimal GetTotal() => Subtotal + DeliveryMethod.Price;
}
