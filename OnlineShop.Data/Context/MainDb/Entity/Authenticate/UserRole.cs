using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShop.Data.Context.MainDb.Entity.Authenticate
{
    public class UserRole : BaseEntity
    {
        public UserRole(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }

    public class UserRoleMapping : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RoleId });

            builder.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);

            builder.ToTable(nameof(UserRole), schema: Common.Library.Constant.DatabaseSchema.Authenticate);
        }
    }
}
