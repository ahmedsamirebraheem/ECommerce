using ECommerce.Domain.Entities.IdentityModule;
using ECommerce.ServiceAbstraction;
using ECommerce.Shared.Common_Result;
using ECommerce.Shared.IdentityDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Service;

public class AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration) : IAuthenticationService
{
    public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Error.InvalidCredentials("User invalidCred");
        }
        var IsPasswordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!IsPasswordValid)
        {
            return Error.InvalidCredentials("Password invalidCred");
        }
        var token = await CreateTokenAsync(user);
        return new UserDto(user.Email!,user.DisplayName, token);


    }

    public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto)
    {
        var user = new ApplicationUser
        {
            Email = registerDto.Email,
            DisplayName = registerDto.DisplayName,
            UserName = registerDto.UserName,
            PhoneNumber = registerDto.PhoneNumber
        };
        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            return result.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();
        }
        var token = await CreateTokenAsync(user);
        return new UserDto(user.Email, user.DisplayName, token);
    }

    private async Task<string> CreateTokenAsync(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Name, user.UserName!)
        };

        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var secretkey = configuration["JwtOptions:SecretKey"];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token= new JwtSecurityToken(
            issuer: configuration["JwtOptions:Issuer"],
            audience: configuration["JwtOptions:Audience"],
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: creds

            );
        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}
