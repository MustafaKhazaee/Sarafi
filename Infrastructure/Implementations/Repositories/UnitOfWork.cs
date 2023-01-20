using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Application.Interfaces.Services;
using Sarafi.Infrastructure.Persistence;

namespace Sarafi.Infrastructure.Implementations.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork (ApplicationDbContext context, IUserService userService)
        {
            AccountRepository = new AccountRepository(context, userService);
            ConnectionRepository = new ConnectionRepository(context, userService);
            ExchangeRateRepository = new ExchangeRateRepository(context, userService);
            MasterAccountRepository = new MasterAccountRepository(context, userService);
            PermissionRepository = new PermissionRepository(context, userService);
            RoleRepository = new RoleRepository(context, userService);
            RolePermissionRepository = new RolePermissionRepository(context, userService);
            UserRepository = new UserRepository(context , userService);
            UserRoleRepository = new UserRoleRepository(context, userService);
            TransactionRepository = new TransactionRepository(context, userService);
            CompanyRepository = new CompanyRepository(context, userService);
            ProvinceRepository = new ProvinceRepository(context, userService);
            ActivityRepository = new ActivityRepository(context, userService);
            NotificationRepository = new NotificationRepository(context, userService);
        }
        public IAccountRepository AccountRepository { set; get; }
        public IConnectionRepository ConnectionRepository { set; get; }
        public IExchangeRateRepository ExchangeRateRepository { set; get; }
        public IMasterAccountRepository MasterAccountRepository { set; get; }
        public IPermissionRepository PermissionRepository { set; get; }
        public IRoleRepository RoleRepository { set; get; }
        public IRolePermissionRepository RolePermissionRepository { set; get; }
        public ITransactionRepository TransactionRepository { set; get; }        
        public IUserRepository UserRepository { set; get; }
        public IUserRoleRepository UserRoleRepository { set; get; }
        public ICompanyRepository CompanyRepository { get; set; }
        public IProvinceRepository ProvinceRepository { get; set; }
        public IActivityRepository ActivityRepository { get; set; }
        public INotificationRepository NotificationRepository { get; set; }
    }
}
