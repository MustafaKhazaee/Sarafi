
using Sarafi.Application.Common.MappingProfile;
using Sarafi.Domain.Entities;
using Sarafi.Domain.Enums;

namespace Sarafi.Application.Applications.Transactions.Dtos
{
    public class TransactionDto : Mappable<Transaction, TransactionDto>
    {
        public long Id { set; get; }
        public decimal Amount { set; get; }
        public decimal Commission { set; get; }
        public long? FromAccountId { set; get; }
        public long? ToAccountId { set; get; }
        public DateTime DateTime { set; get; }
        public TransactionType TransactionType { set; get; }
        public TransactionStatus Status { set; get; }
        public string? ToPerson { set; get; }
        public string? Remarks { set; get; }
        public string? SlipPhoto { set; get; }
    }
}
