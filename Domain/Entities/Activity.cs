
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities;

public class Activity : AggregateRoot, IMultiTenant
{
    public Activity()
    {
        Notifications = new List<Notification>();
    }
    public long UserId { get; private set; } // The activity was created by this user
    public virtual User User { private set; get; }
    public ActivityType ActivityType { get; private set; }
    public string JsonData { private set; get; } // detail of activity as JSON
    public virtual ICollection<Notification> Notifications { get; private set; }
    public long CompanyId { get; set; }

    public void SetCompanyId(long companyId) => CompanyId = companyId;
}
