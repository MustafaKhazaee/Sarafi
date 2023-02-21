
using Sarafi.Application.Common.MappingProfile;
using Sarafi.Domain.Entities;
using Sarafi.Domain.Enums;

namespace Sarafi.Application.Applications.Accounts.Dtos
{
    public class AccountDto : Mappable<Account, AccountDto>
    {
        public long Id { set; get; }
        public long MasterAccountId { set; get; }
        public long UserId { set; get; }
        public string AccountName { set; get; }
        public decimal Balance { set; get; }
        public CurrencyType CurrencyType { set; get; }
        public bool IsLocked { set; get; }
    }
}
