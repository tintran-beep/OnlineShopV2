using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShop.Data.Context.MainDb.Entity.Authenticate
{
    public class UserLogin : BaseEntity
    {
        public UserLogin()
        {

        }
        public Guid UserId { get; set; }
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }

        public virtual User User { get; set; }
    }

    public class UserLoginMapping : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasKey(x => new { x.UserId, x.Provider });

            builder.Property(x => x.Provider).HasMaxLength(256).IsRequired();
            builder.Property(x => x.ProviderKey).HasMaxLength(256).IsRequired();
            builder.Property(x => x.ProviderDisplayName).HasMaxLength(256).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.UserLogins).HasForeignKey(x => x.UserId);

            builder.ToTable(nameof(UserLogin), schema: Common.Library.Constant.DatabaseSchema.Authenticate);
        }
    }
}
