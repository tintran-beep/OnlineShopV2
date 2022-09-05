using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShop.Data.Context.MainDb.Entity.Authenticate
{
    public class Role : BaseEntity
    {
        public Role()
        {
            Id = Guid.NewGuid();
            CreatedDate_Utc = DateTime.UtcNow;
            UpdatedDate_Utc = DateTime.UtcNow;

            RefreshConcurrencyToken();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ConcurrencyToken { get; set; }

        public DateTimeOffset CreatedDate_Utc { get; set; }
        public DateTimeOffset UpdatedDate_Utc { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }

        public virtual void RefreshConcurrencyToken()
        {
            ConcurrencyToken = Guid.NewGuid().ToString().ToLower();
        }
    }

    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.ConcurrencyToken).HasMaxLength(50).IsRequired().IsConcurrencyToken();

            builder.HasMany<UserRole>().WithOne().HasForeignKey(x => x.RoleId);
            builder.HasMany<RoleClaim>().WithOne().HasForeignKey(x => x.RoleId);

            builder.ToTable(nameof(Role), schema: Common.Library.Constant.DatabaseSchema.Authenticate);
        }
    }
}
