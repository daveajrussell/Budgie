using Budgie.Core;
using Budgie.Core.Constants;
using Budgie.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Budgie.Api
{
    public class SeedData
    {
        public static void EnsureSeedData(IApplicationBuilder app)
        {
            Console.WriteLine("Seeding database...");

            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<BudgieDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetRequiredService<BudgieDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                context.Database.Migrate();
                EnsureSeedData(context, userManager).Wait();
            }

            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }

        private static async Task EnsureSeedData(BudgieDbContext context, UserManager<User> userManager)
        {
            if (!context.Roles.Any())
            {
                var admin = new Role
                {
                    Name = BudgieRoles.Admin
                };

                await context.Roles.AddAsync(admin);

                var user = new Role
                {
                    Name = BudgieRoles.User
                };

                await context.AddAsync(user);

                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var root = new User
                {
                    UserName = "root@budgie.com",
                    Email = "root@budgie.com"
                };

                var result = await userManager.CreateAsync(root, "Root!@1");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(root, BudgieRoles.Admin);
                    await userManager.AddToRoleAsync(root, BudgieRoles.User);
                }
            }
        }
    }
}
