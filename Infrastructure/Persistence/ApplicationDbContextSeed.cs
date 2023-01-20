
using Microsoft.EntityFrameworkCore;
using Sarafi.Application.Extensions;
using Sarafi.Domain.Entities;
using Sarafi.Domain.Enums;

namespace Sarafi.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static void SeedDatabase (this ApplicationDbContext applicationDbContext, ModelBuilder builder)
    {
            string salt = "".GetSalt();
            Permission permission = new Permission { Id = 1, PermissionCode = "root" };
            Role role = new Role { Id = 1, RoleName = "System Developer" };
            Province province = new Province { Id = 1, Country = Country.Afghanistan , Name = "Kabul" };
            Company company = new Company { Id = 1, Name = "Brute Force", ProvinceId = 1, Country = Country.Afghanistan };
            User user = new User
            {
                Id = 1,
                Firstname = "Mustafa",
                Lastname = "Khazaee",
                Fathername = "Ahmad",
                Username = "mustafa",
                Password = $"Root_Mustafa@123{salt}".GetHash(),
                Salt = salt,
                Email = "mustafa.khazaee1@gmail.com",
                Mobile1 = "+93747286603",
                Mobile2 = "+93765661711",
                DateOfBirth = DateTime.Now,
                ActivationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(1000),
                Country = Country.Afghanistan,
                ProvinceId = 1,
                CompanyId = 1,
                UserType = UserType.Admin
            };
            UserRole userRole = new UserRole { Id = 1, UserId = 1, RoleId = 1 };
            RolePermission rolePermission = new RolePermission { Id = 1, RoleId = 1, PermissionId = 1 };
            List<Account> accounts = new List<Account>
            {
                new Account { Id = 1, AccountName = "Deposit", UserId = 1, MasterAccountId = 1 },
                new Account { Id = 2, AccountName = "Transfer", UserId = 1, MasterAccountId = 1 },
                new Account { Id = 3, AccountName = "Withdraw", UserId = 1, MasterAccountId = 1 }
            };
            MasterAccount masterAccount = new MasterAccount
            {
                Id = 1,
                Code = "001",
                MasterAccountName = "Hawala",
            };
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
