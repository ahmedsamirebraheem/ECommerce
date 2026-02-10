using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Entities.IdentityModule;

public class Address
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = default!;
}
