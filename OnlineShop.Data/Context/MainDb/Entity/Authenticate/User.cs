using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShop.Data.Context.MainDb.Entity.Authenticate
{
    public class User : BaseEntity
    {
        public User()
        {
            Id = Guid.NewGuid();
            CreatedDate_Utc = DateTime.UtcNow;
            UpdatedDate_Utc = DateTime.UtcNow;

            Status = (int)Common.Library.Enum.UserStatus.New;
            AccessFailedCount = 0;

            RefreshConcurrencyToken();
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public string ConcurrencyToken { get; set; }

        public int Status { get; set; }
        public int AccessFailedCount { get; set; }

        public bool? Enabled2FA { get; set; }

        public DateTimeOffset CreatedDate_Utc { get; set; }
        public DateTimeOffset UpdatedDate_Utc { get; set; }
        public DateTimeOffset? LockedEndDate_Utc { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }

        public virtual void RefreshConcurrencyToken()
        {
            ConcurrencyToken = Guid.NewGuid().ToString().ToLower();
        }
    }

    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
            builder.Property(x => x.FullName).HasMaxLength(256).IsRequired();
            builder.Property(x => x.PasswordHash).HasMaxLength(5000).IsRequired();
            builder.Property(x => x.ConcurrencyToken).HasMaxLength(50).IsConcurrencyToken();

            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.AccessFailedCount).IsRequired();

            builder.HasMany<UserRole>().WithOne().HasForeignKey(x => x.UserId);
            builder.HasMany<UserClaim>().WithOne().HasForeignKey(x => x.UserId);
            builder.HasMany<UserLogin>().WithOne().HasForeignKey(x => x.UserId);
            builder.HasMany<UserToken>().WithOne().HasForeignKey(x => x.UserId);

            builder.ToTable(nameof(User), schema: Common.Library.Constant.DatabaseSchema.Authenticate);
        }
    }
}
