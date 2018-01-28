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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(Configuration[AppBaseUri])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
                options.Filters.Add(new CorsAuthorizationFilterFactory("default"));
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
            var options = new RewriteOptions()
                .AddRedirectToHttps();

            app.UseRewriter(options);

            app.UseCors("default");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
