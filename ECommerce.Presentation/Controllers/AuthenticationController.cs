using ECommerce.Shared.IdentityDtos;
using ECommerce.ServiceAbstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presentation.Controllers;

public class AuthenticationController(IAuthenticationService authenticationService) : ApiBaseController
{
    //login
    //Post : baseUrl/api/authentication/login
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var result = await authenticationService.LoginAsync(loginDto);
        return HandleResult(result);
    }

    //register
    //Post : baseUrl/api/authentication/register
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var result = await authenticationService.RegisterAsync(registerDto);
        return HandleResult(result);
    }
}
