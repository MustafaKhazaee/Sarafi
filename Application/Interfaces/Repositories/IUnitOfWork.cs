
namespace Sarafi.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; set; }
    IConnectionRepository ConnectionRepository { get; set; }
    IExchangeRateRepository ExchangeRateRepository { get; set; }
    IMasterAccountRepository MasterAccountRepository { get; set; }
    IPermissionRepository PermissionRepository { get; set; }
    IRoleRepository RoleRepository { get; set; }
    IRolePermissionRepository RolePermissionRepository { get; set; }
    ITransactionRepository TransactionRepository { set; get; }
    IUserRepository UserRepository { get; set; }
    IUserRoleRepository UserRoleRepository { get; set; }
    ICompanyRepository CompanyRepository { get; set; }
    IProvinceRepository ProvinceRepository { get; set; }
    IActivityRepository ActivityRepository { get; set; }
    INotificationRepository NotificationRepository { get; set; }
}
