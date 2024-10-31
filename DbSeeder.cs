using CologneStore.Constants;
using Microsoft.AspNetCore.Identity;

namespace CologneStore.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<IdentityUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            //Adding some roles to db 


            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Company.ToString()));

            //Create admin user

            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var company = new IdentityUser
            {
                UserName = "company@gmail.com",
                Email = "company@gmail.com",
                EmailConfirmed = true
            };

            var adminInDb = await userManager.FindByEmailAsync(admin.Email);
            if (adminInDb == null)
            {
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }

            var companyInDb = await userManager.FindByEmailAsync(company.Email);
            if (companyInDb == null)
            {
                await userManager.CreateAsync(company, "Company@123");
                await userManager.AddToRoleAsync(company, Roles.Company.ToString());
            }
        }
    }
}
