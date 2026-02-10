using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ECommerce.Domain.Entities.IdentityModule;

public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = null!;
    public Address? Address { get; set; }
}
