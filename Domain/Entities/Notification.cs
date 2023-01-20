
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities;

public class Notification : AuditableEntity
{
    public long ActivityId { get; set; }
    public virtual Activity Activity { get; set; }
    public long UserId { get; set; } // to user
    public virtual User User { get; set; }
    public bool IsRead { set; get; }
}
