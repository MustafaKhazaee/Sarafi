
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities;

public class Province : AuditableEntity
{
    public string Name { set; get; }
    public Country Country { set; get; }
    public virtual ICollection<User> Users { set; get; }
    public virtual ICollection<Company> Companies { set; get; }
}
