using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Common.Model.Authenticate;
using OnlineShop.Data.Context.MainDb.Entity;
using OnlineShop.Data.Context.MainDb.Entity.Authenticate;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Service
{
    public interface IAuthService
    {
        Task<IList<UserModel>> GetUsersAsync();
    }
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(IMapper mapper, IUnitOfWork<MainDbContext> mainUow) : base(mapper, mainUow)
        {

        }

        public async Task<IList<UserModel>> GetUsersAsync()
        {
            var users = await _mainUow.Repository<User>().All().AsNoTracking().ToListAsync();
            return users.Select(x => _mapper.Map<UserModel>(x)).ToList();
        }
    }
}
