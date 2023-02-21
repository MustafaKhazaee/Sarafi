
namespace Sarafi.Domain.Common;

public abstract class AggregateRoot {
    public AggregateRoot() { }

    public long Id { get; private set; }
    public long? CreatedById { get; private set; }
    public DateTime? CreatedDate { get; private set; }  
    public long? ModifiedById { get; private set; }
    public DateTime? ModifiedDate { get; private set; }

    protected void SetId (long id) => Id = id;

    public void SetCreatedDate (DateTime createdDate) => CreatedDate = createdDate;

    public void SetCreatedById (long createdById) => ModifiedById = createdById;

    public void SetModifiedById (long modifiedById) => ModifiedById = modifiedById;

    public void SetModifiedDate (DateTime modifiedDate) => ModifiedDate = modifiedDate;
}
