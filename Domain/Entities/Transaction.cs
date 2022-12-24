
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities
{
    public class Transaction : AuditableEntity
    {
        public decimal Amount { set; get; }
        public decimal Commission { set; get; }
        public long? FromAccountId { set; get; }
        public virtual Account FromAccount { set; get; }
        public long? ToAccountId { set; get; }
        public virtual Account ToAccount { set; get; }
        public decimal ExchangeRate { set; get; } = 1;
        public string? ApprovedBy { set; get; } = "A";
        public bool IsApproved { set; get; } = true;
        public DateTime DateTime { set; get; } = DateTime.Now;
        public TransactionType TransactionType { set; get; }
        public TransactionStatus Status { set; get; } = TransactionStatus.Success;
        public string? ToPerson { set; get; }
        public byte[]? ToPersonFingerPrint { set; get; }
        public string? Remarks { set; get; }
        public string? SlipPhoto { set; get; }
    }
}
