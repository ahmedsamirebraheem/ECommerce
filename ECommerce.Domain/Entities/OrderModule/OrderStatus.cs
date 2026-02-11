using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.OrderModule;

public enum OrderStatus
{
    Pending,
    PaymentReceived,
    PaymentFailed,
}
