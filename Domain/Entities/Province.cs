
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities;

public class Province : AggregateRoot
{
    public Province() {
        Users = new List<User>();
        Companies = new List<Company>();
    }

    public string Name { private set; get; }
    public Country Country { private set; get; } = Country.Afghanistan;
    public virtual ICollection<User> Users { private set; get; }
    public virtual ICollection<Company> Companies { private set; get; }

    public Province(long id, string name, Country country = Country.Afghanistan)
    {
        SetId(id);
        Name = name;
        Country = country;
        Users = new List<User>();
        Companies = new List<Company>();
    }
}
