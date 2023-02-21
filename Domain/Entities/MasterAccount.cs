
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities; 
public class MasterAccount : AggregateRoot, IMultiTenant
{
    public MasterAccount()
    {
        Accounts = new List<Account>();
    }
    public string MasterAccountName { private set; get; }
    public string? Code { private set; get; }
    public virtual ICollection<Account> Accounts { get; private set; }
    public long CompanyId { get; set; }

    public MasterAccount(long id, string masterAccountName, string? code, long companyId)
    {
        SetId(id);
        SetCompanyId(companyId);
        MasterAccountName = masterAccountName;
        Code = code;
        Accounts = new List<Account>();
    }

    public void SetCompanyId(long companyId) => CompanyId = companyId;
}
