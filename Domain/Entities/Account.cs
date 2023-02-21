using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities; 

public class Account : AggregateRoot, IMultiTenant
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
    public long CompanyId { get; set; }
    public Account(long id, string accountName, long userId, long masterAccountId, CurrencyType currencyType, long companyId)
    {
        SetId(id);
        SetCompanyId(companyId);
        AccountName = accountName;
        UserId = userId;
        MasterAccountId = masterAccountId;
        CurrencyType = currencyType;

        TransactionsIn = new List<Transaction>();
        TransactionsOut = new List<Transaction>();
    }

    public void SetCompanyId(long companyId) => CompanyId = companyId;

    public void AddBalance(decimal balance) => Balance += balance;

    public void DeductBalance(decimal balance) => Balance -= balance;
}
