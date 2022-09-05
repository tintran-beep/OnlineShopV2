using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShop.Data.Context.MainDb.Entity.Authenticate
{
    public class RoleClaim : BaseEntity
    {
        public RoleClaim()
        {
            CreatedDate_Utc = DateTime.UtcNow;
            UpdatedDate_Utc = DateTime.UtcNow;

            RefreshConcurrencyToken();
        }

        public Guid RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string ConcurrencyToken { get; set; }

        public DateTimeOffset CreatedDate_Utc { get; set; }
        public DateTimeOffset UpdatedDate_Utc { get; set; }

        public virtual Role Role { get; set; }

        public virtual void RefreshConcurrencyToken()
        {
            ConcurrencyToken = Guid.NewGuid().ToString().ToLower();
        }
    }

    public class RoleClaimMapping : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.HasKey(x => new { x.RoleId, x.ClaimType });

            builder.Property(x => x.ClaimType).HasMaxLength(256).IsRequired();
            builder.Property(x => x.ClaimValue).HasMaxLength(256).IsRequired();
            builder.Property(x => x.ConcurrencyToken).HasMaxLength(50).IsRequired().IsConcurrencyToken();

            builder.HasOne(x => x.Role).WithMany(x => x.RoleClaims).HasForeignKey(x => x.RoleId);

            builder.ToTable(nameof(RoleClaim), schema: Common.Library.Constant.DatabaseSchema.Authenticate);
        }
    }
}
