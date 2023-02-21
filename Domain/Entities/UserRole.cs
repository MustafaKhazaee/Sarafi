
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities; 
public class UserRole : AggregateRoot, IMultiTenant
{
    public UserRole() { }

    public long UserId { private set; get; }
    public virtual User User { private set; get; }
    public long RoleId { private set; get; }
    public virtual Role Role { private set; get; }
    public long CompanyId { set; get; }

    public UserRole(long id, long userId, long roleId, long companyId)
    {
        SetId(id);
        SetCompanyId(companyId);
        UserId = userId;
        RoleId = roleId;
    }

    public void SetCompanyId(long companyId) => CompanyId = companyId;
}
