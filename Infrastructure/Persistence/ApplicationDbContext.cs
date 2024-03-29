﻿
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sarafi.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Sarafi.Domain.Entities;
using System.Reflection;
using Sarafi.Domain.Common;

namespace Sarafi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly long _userId;
    private readonly long _companyId;
    private readonly DateTime _now = DateTime.Now;

    public ApplicationDbContext(DbContextOptions options, IUserService userService) : base(options)
    {
        _userId = userService.GetUserId();
        _companyId = userService.GetCompanyId();
    }
    static ApplicationDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<decimal>().HavePrecision(20, 6); // 6 Decimal points + 14 wholes
        builder.Properties<string>().HaveMaxLength(200);    // Global max length for all string type columns
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
            typeof(AggregateRoot).IsAssignableFrom(i.GenericTypeArguments[0])));
        this.SeedDatabase(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreatedById(_userId);
                    entry.Entity.SetCreatedDate(_now);
                    if (entry.Entity is IMultiTenant)
                    {
                        (entry.Entity as IMultiTenant).SetCompanyId(_companyId);
                    }
                    break;
                case EntityState.Modified:
                    entry.Entity.SetModifiedById(_userId);
                    entry.Entity.SetModifiedDate(_now);
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    private async Task DispatchEvents()
    {
        //while (true)
        //{
        //    var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
        //        .Select(x => x.Entity.DomainEvents)
        //        .Where(x => x != null)
        //        .SelectMany(x => x)
        //        .Where(domainEvent => !domainEvent.IsPublished)
        //        .FirstOrDefault();
        //    if (domainEventEntity == null)
        //        break;

        //    var processorAttributes = domainEventEntity.GetType().GetCustomAttributes(typeof(ProcessedByEventProcessorAttribute), true);
        //    var processorAttribute = (ProcessedByEventProcessorAttribute)processorAttributes.SingleOrDefault();
        //    if (processorAttribute != null)
        //    {
        //        domainEventEntity.IsPublished = true;
        //        continue;
        //    }


        //    domainEventEntity.IsPublished = true;
        //    await _domainEventService.PublishAsync(domainEventEntity);
    }
}

