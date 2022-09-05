using AutoMapper;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Context.MainDb.Entity;

namespace OnlineShop.Service.Infrastructure
{
    public class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork<MainDbContext> _mainUow;
        
        public BaseService(IMapper mapper, IUnitOfWork<MainDbContext> mainUow)
        {
            _mapper = mapper;
            _mainUow = mainUow;
        }
    }

    public static class ServiceHelper
    {
        public static List<Type> GetImplementedServices()
        {
            var services = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => !string.IsNullOrWhiteSpace(x.Namespace) 
                                                                                                && x.Name.EndsWith("Service")
                                                                                                && x.Namespace.StartsWith("OnlineShop.Service")
                                                                                                && x.IsInterface == false
                                                                                                && x.IsInstanceOfType(typeof(BaseService))
                                                                                                && x.GetInterfaces().Length > 0).ToList();

            return services;
        }
    }
}
