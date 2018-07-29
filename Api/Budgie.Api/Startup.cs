using AutoMapper;
using Budgie.Core.Contracts.Security;
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

            var options = new RewriteOptions().AddRedirectToHttps();

            app.UseRewriter(options);
            app.UseAuthentication();

            app.UseCors("SiteCorsPolicy");

            app.UseMvc();
        }
    }
}