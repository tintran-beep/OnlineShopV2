using AutoMapper;
using OnlineShop.Common.Model.Authenticate;
using OnlineShop.Data.Context.MainDb.Entity.Authenticate;

namespace OnlineShop.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}