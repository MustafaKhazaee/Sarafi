
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities;

public class Company : AuditableEntity
{
    public string Name { set; get; }
    public string? Address { set; get; }
    public string? Market { set; get; }
    public string? Floor { set; get; }
    public string? Room { set; get; }
    public string? Email { set; get; }
    public string? Mobile { set; get; }
    public string? Logo { set; get; }
    public Country Country { set; get; }
    public long ProvinceId { set; get; }
    public virtual Province Province { set; get; }
}
