
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities {
    public class UserRole : AuditableEntity {
        public long UserId { set; get; }
        public virtual User User { set; get; }
        public long RoleId { set; get; }
        public virtual Role Role { set; get; }
    }
}
