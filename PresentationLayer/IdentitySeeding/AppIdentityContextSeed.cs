using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace PresentationLayer.IdentitySeeding
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var adminRole = new ApplicationRole()
                {
                    Name = "Admin",
                    CreatedAt = DateTime.Now,
                };
                await roleManager.CreateAsync(adminRole);

                var hrRole = new ApplicationRole()
                {
                    Name = "HR",
                    CreatedAt = DateTime.Now,
                };
                await roleManager.CreateAsync(hrRole);
                var normalUserRole = new ApplicationRole()
                {
                    Name = "Normal User",
                    CreatedAt = DateTime.Now,
                };
                await roleManager.CreateAsync(normalUserRole);
            }
        }
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    Email = "Admin@company.com",
                    EmailConfirmed = true,
                    IsAgree = true,
                    UserName = "Admin",
                };

                await userManager.CreateAsync(admin, "Admin123?");
                await userManager.AddToRoleAsync(admin, "Admin");

                var hr = new ApplicationUser
                {
                    Email = "hr@company.com",
                    EmailConfirmed = true,
                    IsAgree = true,
                    UserName = "HR",
                };

                await userManager.CreateAsync(hr, "Hr123?");
                await userManager.AddToRoleAsync(hr, "HR");
            }
        }
    }
}
