
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities; 
public class RolePermission : AuditableEntity
{
    public RolePermission()
    {

    }
    public long RoleId { get; private set; }
    public virtual Role Role { private set; get; }
    public long PermissionId { private set; get; }
    public virtual Permission Permission { private set; get; }

    public RolePermission(long id, long roleId, long permissionId)
    {
        SetId(id);
        RoleId = roleId;
        PermissionId = permissionId;
    }
}
