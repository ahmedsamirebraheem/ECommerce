using ECommerce.Shared.Common_Result;
using ECommerce.Shared.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.ServiceAbstraction;

public interface IAuthenticationService
{
    Task<Result<UserDto>> LoginAsync(LoginDto loginDto);
    Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto);
    Task<bool> CheckEmailAsync(string email);
    Task<Result<UserDto>> GetUserByEmailAsync(string email);

}
