
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities; 
public class Permission : AuditableEntity
{
	public Permission () { }

    public string PermissionCode { private set; get; }

    public Permission(long id, string permissionCode)
    {
        SetId(id);
        PermissionCode = permissionCode;
    }

    public void SetPermissionCode(string permissionCode) => permissionCode = permissionCode;

}
