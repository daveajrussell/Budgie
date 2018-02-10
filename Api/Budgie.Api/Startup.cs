using System;
using AutoMapper;
using Budgie.Core;
using Budgie.Core.Enums;
using Budgie.Data.Helpers;
using Budgie.Data.Services;
using Budgie.Framework.Facade.Middlewares;
using Budgie.Framework.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
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

            context.SaveChanges();
        }
    }
}