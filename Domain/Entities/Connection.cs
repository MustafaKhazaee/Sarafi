
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities {
    public class Connection : AuditableEntity {
        public long FromUserId { set; get; }
        public virtual User FromUser { get; set; }
        public long ToUserId { set; get; }
        public virtual User ToUser { get; set; }
        public ConnectionStatus ConnectionStatus { get; set; }
    }
}
