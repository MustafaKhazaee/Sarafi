using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Infrastructure.Persistence;

namespace Sarafi.Infrastructure.Implementations.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork (ApplicationDbContext context)
        {
            AccountRepository = new AccountRepository(context);
            ConnectionRepository = new ConnectionRepository(context);
            ExchangeRateRepository = new ExchangeRateRepository(context);
            MasterAccountRepository = new MasterAccountRepository(context);
            PermissionRepository = new PermissionRepository(context);
            RoleRepository = new RoleRepository(context);
            RolePermissionRepository = new RolePermissionRepository(context);
            UserRepository = new UserRepository(context);
            UserRoleRepository = new UserRoleRepository(context);
            TransactionRepository = new TransactionRepository(context);
            CompanyRepository = new CompanyRepository(context);
            ProvinceRepository = new ProvinceRepository(context);
            ActivityRepository = new ActivityRepository(context);
            NotificationRepository = new NotificationRepository(context);
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
