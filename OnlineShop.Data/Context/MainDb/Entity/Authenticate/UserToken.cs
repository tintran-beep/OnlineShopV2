using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShop.Data.Context.MainDb.Entity.Authenticate
{
    public class UserToken : BaseEntity
    {
        public UserToken()
        {
            CreatedDate_Utc = DateTime.UtcNow;
            UpdatedDate_Utc = DateTime.UtcNow;
        }
        public Guid UserId { get; set; }
        public string Provider { get; set; }
        public string TokenName { get; set; }
        public string TokenValue { get; set; }

        public DateTimeOffset TokenExpiredDate_Utc { get; set; }
        public DateTimeOffset CreatedDate_Utc { get; set; }
        public DateTimeOffset UpdatedDate_Utc { get; set; }

        public virtual User User { get; set; }
    }

    public class UserTokenMapping : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(x => new { x.UserId, x.Provider, x.TokenName });

            builder.Property(x => x.Provider).HasMaxLength(256).IsRequired();
            builder.Property(x => x.TokenName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.TokenValue).HasMaxLength(5000).IsRequired();

            builder.Property(x => x.TokenExpiredDate_Utc).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.UserTokens).HasForeignKey(x => x.UserId);

            builder.ToTable(nameof(UserToken), schema: Common.Library.Constant.DatabaseSchema.Authenticate);
        }
    }
}
