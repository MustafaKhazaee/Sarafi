namespace Sarafi.Domain.Common;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
    public long? DeletedById { get; set; }
    public DateTime? DeletedDate { get; set; }

    public void SoftDelete(long? deletedById, DateTime? deletedDate);
    public void UnDelete();
}
