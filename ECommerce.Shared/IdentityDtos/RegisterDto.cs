using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Shared.IdentityDtos;

public record RegisterDto([EmailAddress] string Email,string DisplayName, string UserName, string Password, [Phone] string PhoneNumber);