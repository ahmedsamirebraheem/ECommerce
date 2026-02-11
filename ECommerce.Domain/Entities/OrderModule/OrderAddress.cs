using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.OrderModule;

public class OrderAddress
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Country { get; set; } = null!;
}
