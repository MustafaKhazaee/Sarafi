
namespace Sarafi.Domain.Common;

public abstract class AggregateRoot {
    public AggregateRoot() { }

    public long Id { get; private set; }

    protected void SetId (long id) => Id = id;
}
