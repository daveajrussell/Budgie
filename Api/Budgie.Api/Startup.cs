using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Budgie.Data.Services;
using Microsoft.EntityFrameworkCore;
using Budgie.Data.Helpers;
using Budgie.Framework.Facade.Middlewares;
using Budgie.Framework.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Budgie.Core;

namespace Budgie.Api
{
    public class Startup
    {
        private const string ConnectionStringName = "AppSettings:ConnectionString";
        private const string AppBaseUri = "AppSettings:AppBaseUri";
        private const string IdentityServerBaseUri = "AppSettings:IdentityServerBaseUri";

        public IConfiguration Configuration { get; }

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

            services.AddDbContext<BudgieDbContext>(options =>
                options.UseInMemoryDatabase("budgie"));

            services.AddDevelopmentDataLayer();

            services.AddTransient<ITokenResolverMiddleware, TokenResolverMiddleware>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
                options.Filters.Add(new RequireHttpsAttribute());
                options.Filters.Add(new CorsAuthorizationFilterFactory("SiteCorsPolicy"));
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
                app.UseCors("SiteCorsPolicy");
            }

            app.UseMvc();
        }

        private static void AddTestData(BudgieDbContext context)
        {
            context.Users.Add(new User
            {
                Id = 1
            });

            context.Budgets.Add(new Budget
            {
                UserId = 1,
                Month = 2,
                Year = 2018
            });

            context.SaveChanges();
        }
    }
}
