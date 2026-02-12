using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.BasketDtos;

public record BasketDTO(string Id, 
    ICollection<BasketItemDTO> Items,
 int? DeliveryMethodId,
 string? PaymentIntent,
 string? ClientSecret,
 decimal ShippingCost);
