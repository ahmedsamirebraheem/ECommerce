using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.IdentityDtos;

public record UserDto(string Email,string DisplayName,string Token);

