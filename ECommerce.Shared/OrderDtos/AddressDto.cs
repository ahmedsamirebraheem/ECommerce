using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.OrderDtos;

public record AddressDto(string FirstName, string LastName, string Country, string Street , string City);
