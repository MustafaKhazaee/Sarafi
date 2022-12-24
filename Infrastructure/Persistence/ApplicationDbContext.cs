using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sarafi.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Sarafi.Domain.Entities;
using Sarafi.Domain.Common;
using System.Reflection;
namespace Sarafi.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly long _userId;
        private readonly DateTime _now = DateTime.Now;
        public ApplicationDbContext (DbContextOptions options, IUserService userService) : base (options) {
            _userId = userService.GetUserId ();
        }
        public virtual DbSet<Account> Accounts { set; get; }
        public virtual DbSet<Connection> Connections { set; get; }
        public virtual DbSet<ExchangeRate> ExchangeRate { set; get; }
        public virtual DbSet<MasterAccount> MasterAccounts { set; get; }
        public virtual DbSet<Permission> Permissions { set; get; }
        public virtual DbSet<Role> Roles { set; get; }
        public virtual DbSet<RolePermission> RolePermissions { set; get; }
        public virtual DbSet<Transaction> Transactions { set; get; }
        public virtual DbSet<User> Users { set; get; }
        public virtual DbSet<UserRole> UserRoles { set; get; }
        public virtual DbSet<Activity> Activities { set; get; }
        public virtual DbSet<Province> Provinces { set; get; }
        public virtual DbSet<Company> Companies { set; get; }
        public virtual DbSet<Notification> Notifications { set; get; }
        protected override void ConfigureConventions(ModelConfigurationBuilder builder) =>
            builder.Properties<decimal>().HavePrecision(20, 6); // 6 decimal points + 14 wholes
        protected override void OnModelCreating (ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                typeof(AuditableEntity).IsAssignableFrom(i.GenericTypeArguments[0])));
            this.SeedDatabase(builder);
        }
        public override Task<int> SaveChangesAsync (CancellationToken cancellationToken)
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = _userId;
                        entry.Entity.CreatedDate = _now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedById = _userId;
                        entry.Entity.ModifiedDate = _now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeletedById = _userId;
                        entry.Entity.DeletedDate = _now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
