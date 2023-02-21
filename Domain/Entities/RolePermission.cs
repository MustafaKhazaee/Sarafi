
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities; 
public class RolePermission : AggregateRoot, IMultiTenant
{
    public RolePermission()
    {

    }
    public long RoleId { get; private set; }
    public virtual Role Role { private set; get; }
    public long PermissionId { private set; get; }
    public virtual Permission Permission { private set; get; }
    public long CompanyId { get; set; }

    public RolePermission(long id, long roleId, long permissionId, long companyId)
    {
        SetId(id);
        SetCompanyId(companyId);
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public void SetCompanyId(long companyId) => CompanyId = companyId;
}
