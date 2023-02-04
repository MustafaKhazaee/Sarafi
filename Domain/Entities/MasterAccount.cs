
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities; 
public class MasterAccount : AuditableEntity
{
    public MasterAccount()
    {
        Accounts = new List<Account>();
    }
    public string MasterAccountName { private set; get; }
    public string? Code { private set; get; }
    public virtual ICollection<Account> Accounts { get; private set; }

    public MasterAccount(long id, string masterAccountName, string? code = null)
    {
        SetId(id);
        MasterAccountName = masterAccountName;
        Code = code;

        Accounts = new List<Account>();
    }
}
