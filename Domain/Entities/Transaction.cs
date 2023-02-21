
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities;

public class Transaction : AggregateRoot, IMultiTenant
{
    public Transaction() { }

    public decimal Amount { private set; get; }
    public decimal Commission { private set; get; } = 0;
    public long? FromAccountId { private set; get; }
    public virtual Account FromAccount { private set; get; }
    public long? ToAccountId { private set; get; }
    public virtual Account ToAccount { private set; get; }
    public long? UserId { private set; get; }
    public virtual User User { private set; get; }
    public decimal ExchangeRate { private set; get; } = 1;
    public string? ApprovedBy { private set; get; }
    public bool IsApproved { private set; get; } = true;
    public DateTime DateTime { private set; get; } = DateTime.Now;
    public TransactionType TransactionType { private set; get; }
    public TransactionStatus Status { private set; get; } = TransactionStatus.Success;
    public string? ToPerson { private set; get; }
    public byte[]? ToPersonFingerPrint { private set; get; }
    public string? Remarks { private set; get; }
    public string? SlipPhoto { private set; get; }
    public long CompanyId { get; set; }

    public void SetCompanyId(long companyId) => CompanyId = companyId;

}
