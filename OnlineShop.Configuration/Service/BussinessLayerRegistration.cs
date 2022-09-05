using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Configuration.Service
{
    public static class BussinessLayerRegistration
    {
        public static IServiceCollection AddBussinessLayer(this IServiceCollection services)
        {
            var implementedServices = ServiceHelper.GetImplementedServices();
            if (implementedServices != null && implementedServices.Any())
            {
                implementedServices.ForEach(assignedTypes =>
                {
                    var serviceType = assignedTypes.GetInterfaces().FirstOrDefault(x => nameof(x).Contains(nameof(assignedTypes)));
                    if (serviceType != null)
                        services.AddScoped(serviceType, assignedTypes);
                });
            }
            return services;
        }
    }
}
