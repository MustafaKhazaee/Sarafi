
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities {
    public class Permission : AuditableEntity {
        public string PermissionCode { set; get; }
    }
}
