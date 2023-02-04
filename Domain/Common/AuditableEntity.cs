
namespace Sarafi.Domain.Common;

public abstract class AuditableEntity {
    public AuditableEntity() { }

    public long Id { get; private set; }
    public long? CompanyId { private set; get; }
    public long? CreatedById { get; private set; }
    public DateTime? CreatedDate { get; private set; }  
    public long? ModifiedById { get; private set; }
    public DateTime? ModifiedDate { get; private set; }
    public bool IsDeleted { get; private set; } = false;
    public long? DeletedById { get; private set; }
    public DateTime? DeletedDate { get; private set; }

    protected void SetId (long id) => Id = id;

    public void SetCreatedDate (DateTime createdDate) => CreatedDate = createdDate;

    public void SetCreatedById (long createdById) => ModifiedById = createdById;

    public void SetCompanyId (long companyId) => CompanyId = companyId;

    public void SetModifiedById (long modifiedById) => ModifiedById = modifiedById;

    public void SetModifiedDate (DateTime modifiedDate) => ModifiedDate = modifiedDate;
}
