using Data.Abstractions;
using Data.Factories;
using Data.Services;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Helpers
{
    public static class ServiceCollectionExtensionMethods
    {
        public static void AddDataLayer(this IServiceCollection services)
        {
            services.AddScoped<RepositoryFactories>();
            services.AddScoped<IDesignTimeDbContextFactory<CoreDbContext>, CoreDbContextFactory>();
            services.AddTransient<IRepositoryProvider, RepositoryProvider>();
            services.AddTransient<IUow, Uow>();
        }
    }
}
