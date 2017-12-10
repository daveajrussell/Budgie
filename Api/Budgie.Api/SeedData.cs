using Budgie.Core;
using Budgie.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
                }
            }
        }
    }
}
