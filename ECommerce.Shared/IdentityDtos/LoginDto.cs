using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Shared.IdentityDtos;

public record LoginDto([EmailAddress] string Email, string Password);
