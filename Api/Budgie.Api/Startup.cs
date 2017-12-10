using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Budgie.Data.Services;
using Budgie.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Budgie.Data.Helpers;

namespace Budgie.Api
{
    public class Startup
    {
        private const string ConnectionStringName = "AppSettings:ConnectionString";
        private const string AppBaseUri = "AppSettings:AppBaseUri";

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
            services.AddDbContext<BudgieDbContext>(options =>
                options.UseSqlServer(Configuration[ConnectionStringName]));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<BudgieDbContext>()
                .AddDefaultTokenProviders();

            services.AddDataLayer();

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration[ConnectionStringName];
                    options.RequireHttpsMetadata = true;

                    options.ApiName = Framework.Security.BudgieIdentityConstants.ApiName;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(Configuration[AppBaseUri])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitialiseDatabase(app);

            app.UseCors("default");
            app.UseAuthentication();
            app.UseMvc();
        }

        private void InitialiseDatabase(IApplicationBuilder app)
        {
            SeedData.EnsureSeedData(app);
        }
    }
}
