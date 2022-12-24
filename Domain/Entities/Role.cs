
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities {
    public class Role : AuditableEntity {
        public string RoleName { set; get; }
        public virtual ICollection<UserRole> UserRoles { set; get; }
        public virtual ICollection<RolePermission> RolePermissions { set; get; }
    }
}
