
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities; 
public class Permission : AggregateRoot, IMultiTenant
{
	public Permission () { }

    public string PermissionCode { private set; get; }
    public long CompanyId { get; set; }

    public Permission(long id, string permissionCode, long companyId)
    {
        SetId(id);
        SetCompanyId(companyId);
        PermissionCode = permissionCode;
    }

    public void SetPermissionCode(string permissionCode) => PermissionCode = permissionCode;

    public void SetCompanyId(long companyId) => CompanyId = companyId;
}
