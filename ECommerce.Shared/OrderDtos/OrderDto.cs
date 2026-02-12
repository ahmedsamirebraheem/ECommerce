using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.OrderDtos;

public record OrderDto(string BasketId,int DeliveryMethodId,AddressDto Address );

