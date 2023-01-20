
namespace Sarafi.Domain.Common;

public abstract class AuditableEntity {
    public long Id { get; set; }
    public long CompanyId { set; get; }
    public long CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
    public long ModifiedById { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public long? DeletedById { get; set; }
    public DateTime? DeletedDate { get; set; }
}
