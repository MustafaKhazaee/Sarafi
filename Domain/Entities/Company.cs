
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities;

public class Company : AggregateRoot
{
    public Company() { }

    public string Name { private set; get; }
    public string? Address { private set; get; }
    public string? Market { private set; get; }
    public string? Floor { private set; get; }
    public string? Room { private set; get; }
    public string? Email { private set; get; }
    public string? Mobile { private set; get; }
    public string? Logo { private set; get; }
    public Country Country { private set; get; }
    public long ProvinceId { private set; get; }
    public virtual Province Province { private set; get; }

    public Company(long id, string name, long provinceId, Country country = Country.Afghanistan)
    {
        SetId(id);
        Name = name;
        Country = country;
        ProvinceId = provinceId;
    }
}
