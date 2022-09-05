using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Common.Model.Authenticate
{
    public class UserModel
    {
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
    }
}
