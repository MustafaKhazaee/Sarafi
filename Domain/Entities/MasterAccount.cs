
using Sarafi.Domain.Common;

namespace Sarafi.Domain.Entities {
    public class MasterAccount : AuditableEntity {
        public string MasterAccountName { set; get; }
        public string? Code { set; get; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
