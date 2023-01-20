using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;
namespace Sarafi.Domain.Entities; 

public class Account : AuditableEntity
{
    public long MasterAccountId { set; get; }
    public virtual MasterAccount Master { set; get; }
    public long UserId { set; get; }
    public virtual User User { set; get; }
    public string AccountName { set; get; }
    public decimal Balance { set; get; } = 0.0M;
    public CurrencyType CurrencyType { set; get; }
    public bool IsLocked { set; get; } = false;
    public virtual ICollection<Transaction> TransactionsIn { set; get; }
    public virtual ICollection<Transaction> TransactionsOut { set; get; }
}
