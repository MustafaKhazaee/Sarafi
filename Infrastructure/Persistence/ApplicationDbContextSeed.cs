
using Microsoft.EntityFrameworkCore;
using Sarafi.Application.Extensions;
using Sarafi.Domain.Entities;
using Sarafi.Domain.Enums;

namespace Sarafi.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static void SeedDatabase(this ApplicationDbContext applicationDbContext, ModelBuilder builder)
    {
        string salt = "".GetSalt();
        Permission permission = new(1, "root", 1);
        Role role = new(1, "System Developer", 1);
        Province province = new(1, "Kabul");
        Company company = new(1, "Brute Force", 1);
        User user = new(
            1, "Mustafa", "Khazaee", "Ahmad",
            "mustafa.khazaee1@gmail.com",
            "mustafa", $"Root_Mustafa@123{salt}".GetHash(), salt, null,
            DateTime.Now, DateTime.Now, DateTime.Now.AddYears(8),
            "+93747286603", "+93765661711", null,
            UserType.Admin, Country.Afghanistan, 1, 1
        );
        UserRole userRole = new(1, 1, 1, 1);
        RolePermission rolePermission = new(1, 1, 1, 1);
        List<Account> accounts = new List<Account>
        {
            new Account (1, "Deposit", 1, 1, CurrencyType.Afghani, 1),
            new Account (2, "Transfer", 1, 1, CurrencyType.Euro, 1),
            new Account (3, "Anything", 1, 1, CurrencyType.USDollar, 1),
        };
        MasterAccount masterAccount = new(1, "001", "General", 1);
        builder.Entity<Province>().HasData(province);
        builder.Entity<Company>().HasData(company);
        builder.Entity<Permission>().HasData(permission);
        builder.Entity<Role>().HasData(role);
        builder.Entity<User>().HasData(user);
        builder.Entity<UserRole>().HasData(userRole);
        builder.Entity<RolePermission>().HasData(rolePermission);
        builder.Entity<MasterAccount>().HasData(masterAccount);
        builder.Entity<Account>().HasData(accounts);
    }
}
