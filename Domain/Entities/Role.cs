
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities; 
public class Role : AuditableEntity
{
    public Role() {
        UserRoles = new List<UserRole>();
        RolePermissions = new List<RolePermission>();
    }

    public string RoleName { private set; get; }
    public virtual ICollection<UserRole> UserRoles { private set; get; }
    public virtual ICollection<RolePermission> RolePermissions { private set; get; }

    public Role(long id, string roleName)
    {
        SetId(id);
        RoleName = roleName;

        UserRoles = new List<UserRole>();
        RolePermissions = new List<RolePermission>();
    }

    public void SetRoleName (string roleName) => RoleName = roleName;
}
