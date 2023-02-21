using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Application.Interfaces.Services;
using Sarafi.Infrastructure.Persistence;

namespace Sarafi.Infrastructure.Implementations.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork (ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            AccountRepository = new AccountRepository(_context, userService);
            ConnectionRepository = new ConnectionRepository(_context, userService);
            ExchangeRateRepository = new ExchangeRateRepository(_context, userService);
            MasterAccountRepository = new MasterAccountRepository(_context, userService);
            PermissionRepository = new PermissionRepository(_context, userService);
            RoleRepository = new RoleRepository(_context, userService);
            RolePermissionRepository = new RolePermissionRepository(_context, userService);
            UserRepository = new UserRepository(_context , userService);
            UserRoleRepository = new UserRoleRepository(_context, userService);
            TransactionRepository = new TransactionRepository(_context, userService);
            CompanyRepository = new CompanyRepository(_context, userService);
            ProvinceRepository = new ProvinceRepository(_context, userService);
            ActivityRepository = new ActivityRepository(_context, userService);
            NotificationRepository = new NotificationRepository(_context, userService);
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

        public async Task<int> SaveChangesAsync(CancellationToken cancellation) => await _context.SaveChangesAsync(cancellation);
    }
}
