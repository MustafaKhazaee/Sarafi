
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities; 
public class Role : AggregateRoot, IMultiTenant
{
    public Role() {
        UserRoles = new List<UserRole>();
        RolePermissions = new List<RolePermission>();
    }

    public string RoleName { private set; get; }
    public virtual ICollection<UserRole> UserRoles { private set; get; }
    public virtual ICollection<RolePermission> RolePermissions { private set; get; }
    public long CompanyId { get; set; }

    public Role(long id, string roleName, long companyId)
    {
        SetId(id);
        SetCompanyId(companyId);
        RoleName = roleName;

        UserRoles = new List<UserRole>();
        RolePermissions = new List<RolePermission>();
    }

    public void SetRoleName (string roleName) => RoleName = roleName;

    public void SetCompanyId(long companyId) => CompanyId = companyId;
}
