using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;
namespace Sarafi.Domain.Entities; 

public class Account : AuditableEntity
{
    public Account()
    {
        TransactionsIn = new List<Transaction>();
        TransactionsOut = new List<Transaction>();
    }

    public long MasterAccountId { private set; get; }
    public virtual MasterAccount Master { private set; get; }
    public long UserId { private set; get; }
    public virtual User User { private set; get; }
    public string AccountName { private set; get; }
    public decimal Balance { private set; get; } = 0.0M;
    public CurrencyType CurrencyType { private set; get; } = CurrencyType.Afghani;
    public bool IsLocked { private set; get; } = false;
    public virtual ICollection<Transaction> TransactionsIn { private set; get; }
    public virtual ICollection<Transaction> TransactionsOut { private set; get; }

    public Account(long id, string accountName, long userId, long masterAccountId, CurrencyType currencyType = CurrencyType.Afghani)
    {
        SetId(id);
        AccountName = accountName;
        UserId = userId;
        MasterAccountId = masterAccountId;
        CurrencyType = currencyType;

        TransactionsIn = new List<Transaction>();
        TransactionsOut = new List<Transaction>();
    }
}
