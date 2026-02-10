using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.IdentityData.DataSeed;

public class IdentityDataInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IDataInitializer
{
    public async Task InitializeAsync()
    {
        try
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

            }
            if (!userManager.Users.Any())
            {
                var user1 = new ApplicationUser
                {
                    DisplayName = "Ahmed Samir",
                    UserName = "ahmedsamir",
                    Email = "ahmedsamir@gamail.com",
                    PhoneNumber = "01000349539"
                };
                var user2 = new ApplicationUser
                {
                    DisplayName = "Karim Samir",
                    UserName = "karimsamir",
                    Email = "karimsamir@gamail.com",
                    PhoneNumber = "01288849606"
                };

                await userManager.CreateAsync(user1, "P@ssw0rd");
                await userManager.CreateAsync(user2, "P@ssw0rd");

                await userManager.AddToRoleAsync(user1, "SuperAdmin");
                await userManager.AddToRoleAsync(user2, "Admin");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during identity data initialization: {ex.Message}");
        }
    }
}
