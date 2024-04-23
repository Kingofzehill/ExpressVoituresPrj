using ExpressVoitures.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExpressVoitures.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ExpressVoitures.Models
{
    /// <summary>
    /// SeedData Identity Class
    /// Initialize datas of users identity. 
    /// </summary>
    /// <remarks>Application datas are managed in ExpressVoituresContext.cs // SeedData.cs.</remarks>
    public static class SeedDataIdentity
    {
        private const string AdminUser = "Admin";
        private const string AdminPassword = "Password123*";

        /*public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));

            IdentityUser user = await userManager.FindByIdAsync(AdminUser);

            if (user == null)
            {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, AdminPassword);
            }
        }*/
    }
}
