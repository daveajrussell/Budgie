using System;
using System.Collections.Generic;
using AutoMapper;
using Budgie.Core;
using Budgie.Core.Contracts.Security;
using Budgie.Core.Enums;
using Budgie.Data.Helpers;
using Budgie.Data.Services;
using Budgie.Framework.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Budgie.Api
{
    public class Startup
    {
        private const string ConnectionStringName = "AppSettings:ConnectionString";
        private const string AppBaseUri = "AppSettings:AppBaseUri";
        private const string IdentityServerBaseUri = "AppSettings:IdentityServerBaseUri";

        public IConfiguration Configuration
        {
            get;
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper();

            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.WithOrigins(Configuration[AppBaseUri]);
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("SiteCorsPolicy"));
            });

            services.AddDbContext<BudgieDbContext>(options =>
                options.UseInMemoryDatabase("budgie"));

            services.AddDevelopmentDataLayer();

            services.AddTransient<ITokenResolverMiddleware, DevTokenResolverMiddleware>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper();

            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.WithOrigins(Configuration[AppBaseUri]);
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
                options.Filters.Add(new CorsAuthorizationFilterFactory("SiteCorsPolicy"));
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddDbContext<BudgieDbContext>(options =>
                options.UseSqlServer(Configuration[ConnectionStringName]));

            services.AddDataLayer();

            services.AddTransient<ITokenResolverMiddleware, TokenResolverMiddleware>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration[IdentityServerBaseUri];
                    options.RequireHttpsMetadata = true;
                    options.ApiName = BudgieIdentityConstants.ApiName;
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<BudgieDbContext>();
                    AddTestData(context);
                }
            }

            if (env.IsProduction())
            {
                var options = new RewriteOptions()
                    .AddRedirectToHttps();

                app.UseRewriter(options);
                app.UseAuthentication();
            }

            app.UseCors("SiteCorsPolicy");
            app.UseMvc();
        }

        private static void AddTestData(BudgieDbContext context)
        {
            context.Users.Add(new User
            {
                Id = 1
            });

            context.Categories.Add(new Category
            {
                Id = 1,
                UserId = 1,
                DateAdded = DateTime.UtcNow,
                Colour = "0f0f0f",
                Name = "Dedicated test",
                Type = CategoryType.Dedicated
            });

            context.Categories.Add(new Category
            {
                Id = 2,
                UserId = 1,
                DateAdded = DateTime.UtcNow,
                Colour = "0f0f0f",
                Name = "Income test",
                Type = CategoryType.Income,
                Recurring = true,
                RecurringDate = DateTime.UtcNow,
                RecurringValue = 1000
            });

            context.Categories.Add(new Category
            {
                Id = 3,
                UserId = 1,
                DateAdded = DateTime.UtcNow,
                Colour = "0f0f0f",
                Name = "Variable test",
                Type = CategoryType.Variable
            });

            context.Categories.Add(new Category
            {
                Id = 4,
                UserId = 1,
                DateAdded = DateTime.UtcNow,
                Colour = "0f0f0f",
                Name = "Saving test",
                Type = CategoryType.Savings,
                Recurring = true,
                RecurringDate = DateTime.UtcNow,
                RecurringValue = 100
            });

            var income = new Income
            {
                Id = 1,
                UserId = 1,
                CategoryId = 2,
                Date = DateTime.UtcNow,
                Total = 1000
            };

            context.Incomes.Add(income);

            var dedicated = new Outgoing
            {
                Id = 1,
                UserId = 1,
                Actual = 0,
                CategoryId = 1,
                Budgeted = 100,
                Date = DateTime.UtcNow
            };

            context.Outgoings.Add(dedicated);

            var variable = new Outgoing
            {
                Id = 2,
                UserId = 1,
                Actual = 0,
                CategoryId = 3,
                Budgeted = 250,
                Date = DateTime.UtcNow
            };

            context.Outgoings.Add(variable);

            var saving = new Saving
            {
                Id = 1,
                UserId = 1,
                CategoryId = 4,
                Total = 250,
                Date = DateTime.UtcNow
            };

            context.Savings.Add(saving);

            var variableTransactionOne = new Transaction
            {
                Id = 1,
                UserId = 1,
                Amount = 100,
                Date = DateTime.UtcNow,
                CategoryId = 3,
                Notes = "Test"
            };

            var variableTransactionTwo = new Transaction
            {
                Id = 2,
                UserId = 1,
                Amount = 100,
                Date = DateTime.UtcNow,
                CategoryId = 3,
                Notes = "Test 2"
            };

            context.Transactions.AddRangeAsync(variableTransactionOne, variableTransactionTwo);

            context.Budgets.Add(new Budget
            {
                Id = 1,
                Year = 2018,
                Month = 2,
                UserId = 1,
                Incomes = new List<Income> { income },
                Outgoings = new List<Outgoing> { dedicated, variable },
                Savings = new List<Saving> { saving },
                Transactions = new List<Transaction> { variableTransactionOne, variableTransactionTwo }
            });

            context.SaveChanges();
        }
    }
}