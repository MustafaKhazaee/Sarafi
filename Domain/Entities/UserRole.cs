
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities; 
public class UserRole : AuditableEntity
{
    public UserRole() { }

    public long UserId { private set; get; }
    public virtual User User { private set; get; }
    public long RoleId { private set; get; }
    public virtual Role Role { private set; get; }

    public UserRole(long id, long userId, long roleId)
    {
        SetId(id);
        UserId = userId;
        RoleId = roleId;
    }
}
