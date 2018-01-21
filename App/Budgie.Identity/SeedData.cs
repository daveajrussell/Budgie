using System;
using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Budgie.Data.Services;
using Budgie.Core;
using Budgie.Core.Constants;

namespace Budgie.Identity
{
    public class SeedData
    {
        public static void EnsureSeedData(IApplicationBuilder app)
        {
            Console.WriteLine("Seeding database...");

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                EnsureSeedData(context);

                var budgieContext = scope.ServiceProvider.GetRequiredService<BudgieDbContext>();
                budgieContext.Database.Migrate();
                EnsureSeedData(budgieContext);
            }

            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }

        private static void EnsureSeedData(BudgieDbContext context)
        {
            if (!context.Roles.Any())
            {
                Console.WriteLine("Roles being populated");

                context.Roles.Add(new Role
                {
                    Name = nameof(BudgieRoles.Admin),
                    NormalizedName = BudgieRoles.Admin.ToUpper()
                });

                context.Roles.Add(new Role
                {
                    Name = nameof(BudgieRoles.User),
                    NormalizedName = "USER"
                });

                context.SaveChanges();
            }
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                Console.WriteLine("Clients being populated");
                foreach (var client in Config.GetClients().ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Clients already populated");
            }

            if (!context.IdentityResources.Any())
            {
                Console.WriteLine("IdentityResources being populated");
                foreach (var resource in Config.GetIdentityResources().ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("IdentityResources already populated");
            }

            if (!context.ApiResources.Any())
            {
                Console.WriteLine("ApiResources being populated");
                foreach (var resource in Config.GetApiResources().ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("ApiResources already populated");
            }
        }
    }
}