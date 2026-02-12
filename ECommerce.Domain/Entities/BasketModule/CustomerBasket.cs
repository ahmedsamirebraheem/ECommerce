using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.BasketModule;

public class CustomerBasket
{
    public string Id { get; set; } = null!;
    public ICollection<BasketItem> Items { get; set; } = [];
    public int? DeliveryMethodId { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
    public decimal ShippingCost { get; set; }
}
