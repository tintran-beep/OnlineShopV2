using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Data.Context
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
