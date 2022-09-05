using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Context.MainDb.Entity.Authenticate;

namespace OnlineShop.Data.Context.MainDb.Entity
{
    public class MainDbContext : BaseDbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {

        }

        #region Authenticate
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Authenticate
            new RoleMapping().Configure(builder.Entity<Role>());
            new UserMapping().Configure(builder.Entity<User>());
            new UserRoleMapping().Configure(builder.Entity<UserRole>());
            new UserClaimMapping().Configure(builder.Entity<UserClaim>());
            new UserLoginMapping().Configure(builder.Entity<UserLogin>());
            new UserTokenMapping().Configure(builder.Entity<UserToken>());
            new RoleClaimMapping().Configure(builder.Entity<RoleClaim>());
            #endregion

            base.OnModelCreating(builder);
        }
    }
}
