using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Identity
{
    public class SeedIdentity
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            var username = configuration["Data:AdminUser:username"];
            var password = configuration["Data:AdminUser:password"];
            var email = configuration["Data:AdminUser:email"];
            var role = configuration["Data:AdminUser:role"];

            var usernameR = configuration["Data:RegularUser:username"];
            var passwordR = configuration["Data:RegularUser:password"];
            var emailR = configuration["Data:RegularUser:email"];
            var roleR = configuration["Data:RegularUser:role"];

            if (await userManager.FindByNameAsync(username) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));//role oluşturma

                var user = new ApplicationUser()
                {
                    UserName = username,
                    Email = email,
                    FirstName = "Admin",
                    LastName = "Admin",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }

            if (await userManager.FindByNameAsync(usernameR) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleR));

                var user = new ApplicationUser()
                {
                    UserName = usernameR,
                    Email = emailR,
                    FirstName = "User",
                    LastName = "User",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(user, passwordR);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleR);
                }
            }
        }
    }
}
