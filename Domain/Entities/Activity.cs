
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities;

public class Activity : AuditableEntity
{
    public long UserId { get; set; } // The activity was created by this user
    public virtual User User { set; get; }
    public ActivityType ActivityType { get; set; }
    public string JsonData { set; get; } // detail of activity as JSON
    public virtual ICollection<Notification> Notifications { get; set; }
}
