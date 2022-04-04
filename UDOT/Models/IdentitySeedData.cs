using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UDOT.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "group37";
        private const string adminPassword = "SecurePasswordFor37!";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppIdentityDbContext _context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<AppIdentityDbContext>();

            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminUser);

            if (user== null)
            {
                user = new IdentityUser(adminUser);

                user.Email = "admin@yeet.com";
                user.PhoneNumber = "555-1234";

                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
