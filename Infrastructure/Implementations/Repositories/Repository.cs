using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Sarafi.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;

namespace Sarafi.Infrastructure.Implementations.Repositories;

public class Repository<T> : IRepository<T> where T : AuditableEntity
{
    private readonly ApplicationDbContext context;

    public IQueryable<T> Query => context.Set<T>().AsQueryable();

    public Repository(ApplicationDbContext _context) { context = _context; }

    public async Task<int> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(entity, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);            
    }

    public async Task<int> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await context.Set<T>().AddRangeAsync(entities, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        await context.Set<T>().CountAsync(cancellationToken);

    public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool includeSoftDeleted = false) =>
        await context.Set<T>().Where(e => includeSoftDeleted || !e.IsDeleted).Where(predicate).AsNoTracking().ToListAsync(cancellationToken);

    public async Task<T?> FindByIdAsync(long Id, CancellationToken cancellationToken = default, bool includeSoftDeleted = false) =>
        await context.Set<T>().FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool includeSoftDeleted = false) =>
        await context.Set<T>().FirstOrDefaultAsync<T>(predicate, cancellationToken);

    public async Task<List<T>> GetFilteredPageAsync(Expression<Func<T, bool>> predicate, int pageIndex = 0, int pageSize = 10, CancellationToken cancellationToken = default, bool includeSoftDeleted = false) =>
        await context.Set<T>().Where(predicate).Skip(pageSize * pageIndex).Take(pageSize).AsNoTracking().ToListAsync(cancellationToken);

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default, bool includeSoftDeleted = false) =>
        await context.Set<T>().Where(e => includeSoftDeleted || !e.IsDeleted).AsNoTracking().ToListAsync(cancellationToken);

    public async Task<int> RemoveAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        await context.Set<T>().Where(predicate).ExecuteDeleteAsync(cancellationToken);

    public async Task<int> UpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCall, CancellationToken cancellationToken = default) =>
        await context.Set<T>().Where(predicate).ExecuteUpdateAsync(setPropertyCall, cancellationToken);

    public async Task<int> SoftDeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        await UpdateAsync(predicate, e => e.SetProperty(e => e.IsDeleted, d => true), cancellationToken);

    public void MarkAsAdded(T entity) => context.Entry(entity).State = EntityState.Added;

    public void MarkAsDeleted(T entity) => context.Entry(entity).State = EntityState.Deleted;

    public void MarkAsDetached(T entity) => context.Entry(entity).State = EntityState.Detached;

    public void MarkAsModified(T entity) => context.Entry(entity).State = EntityState.Modified;

    public void MarkAsUnchanged(T entity) => context.Entry(entity).State = EntityState.Unchanged;

    public async ValueTask DisposeAsync() => await context.DisposeAsync();
}
