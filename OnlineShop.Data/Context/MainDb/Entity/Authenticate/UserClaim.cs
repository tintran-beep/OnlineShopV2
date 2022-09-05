using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShop.Data.Context.MainDb.Entity.Authenticate
{
    public class UserClaim : BaseEntity
    {
        public UserClaim()
        {
            CreatedDate_Utc = DateTime.UtcNow;
            UpdatedDate_Utc = DateTime.UtcNow;

            RefreshConcurrencyToken();
        }

        public Guid UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string ConcurrencyToken { get; set; }

        public DateTimeOffset CreatedDate_Utc { get; set; }
        public DateTimeOffset UpdatedDate_Utc { get; set; }

        public virtual User User { get; set; }

        public virtual void RefreshConcurrencyToken()
        {
            ConcurrencyToken = Guid.NewGuid().ToString().ToLower();
        }        
    }

    public class UserClaimMapping : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.HasKey(x => new { x.UserId, x.ClaimType });

            builder.Property(x => x.ClaimType).HasMaxLength(256).IsRequired();
            builder.Property(x => x.ClaimValue).HasMaxLength(256).IsRequired();
            builder.Property(x => x.ConcurrencyToken).HasMaxLength(50).IsRequired().IsConcurrencyToken();

            builder.HasOne(x => x.User).WithMany(x => x.UserClaims).HasForeignKey(x => x.UserId);

            builder.ToTable(nameof(UserClaim), schema: Common.Library.Constant.DatabaseSchema.Authenticate);
        }
    }
}
