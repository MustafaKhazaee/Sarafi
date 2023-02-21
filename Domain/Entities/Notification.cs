
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities;

public class Notification : AggregateRoot, IMultiTenant
{
    public long ActivityId { get; private set; }
    public virtual Activity Activity { get; private set; }
    public long UserId { get; private set; }
    public virtual User User { get; private set; }
    public bool IsRead { private set; get; }
    public long CompanyId { get; set; }

    public void SetCompanyId(long companyId) => CompanyId = companyId;
}
