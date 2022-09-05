using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Data.Context.MainDb.Entity;
using OnlineShop.Data.Infrastructure;

namespace OnlineShop.Configuration.Service
{
    public static class DataAccessLayerRegistration
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork<MainDbContext>, UnitOfWork<MainDbContext>>();
            services.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MainDbConnection")));

            return services;
        }
    }
}
