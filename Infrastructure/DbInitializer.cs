using Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DbInitializer
    {
        public static void SeedAdmin(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Role
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole("Admin");
                roleManager.CreateAsync(role).Wait();
            }

            // Seed User
            var adminEmail = "admin@library.com";
            if (userManager.FindByEmailAsync(adminEmail).Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(user, "Admin@123").Result; // Password

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }

}
