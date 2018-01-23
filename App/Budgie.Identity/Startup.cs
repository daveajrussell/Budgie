using System.Reflection;
using Budgie.Core;
using Budgie.Data.Services;
using Budgie.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Budgie.Identity.Security;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace Budgie.Identity
{
    public class Startup
    {
        private const string ConnectionStringName = "AppSettings:ConnectionString";
        private const string GoogleClientId = "AppSettings:GoogleClientId";
        private const string GoogleSecret = "AppSettings:GoogleClientSecret";
        private const string FacebookClientId = "AppSettings:FacebookClientId";
        private const string FacebookSecret = "AppSettings:FacebookSecret";

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
            var connectionString = Configuration[ConnectionStringName];
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddMvc();

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddDbContext<BudgieDbContext>(options =>
                options.UseSqlServer(connectionString,
                sql => sql.MigrationsAssembly(migrationsAssembly)));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<BudgieDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IProfileService, BudgieProfileService>();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddAspNetIdentity<User>()
                .AddProfileService<BudgieProfileService>();

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.ClientId = Configuration[GoogleClientId];
                    options.ClientSecret = Configuration[GoogleSecret];
                });

            services.AddAuthentication()
                .AddFacebook("Facebook", options =>
                {
                    options.ClientId = Configuration[FacebookClientId];
                    options.ClientSecret = Configuration[FacebookSecret];
                });

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitialiseDatabase(app);

            var options = new RewriteOptions()
                .AddRedirectToHttps();

            app.UseRewriter(options);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitialiseDatabase(IApplicationBuilder app)
        {
            SeedData.EnsureSeedData(Configuration, app);
        }
    }
}
