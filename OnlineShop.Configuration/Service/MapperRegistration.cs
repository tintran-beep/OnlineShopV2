using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Mapper;

namespace OnlineShop.Configuration.Service
{
    public static class MapperRegistration
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
