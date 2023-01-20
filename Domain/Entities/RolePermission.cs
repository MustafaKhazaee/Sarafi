
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities; 
public class RolePermission : AuditableEntity
{
    public long RoleId { get; set; }
    public virtual Role Role { set; get; }
    public long PermissionId { set; get; }
    public virtual Permission Permission { set; get; }
}
