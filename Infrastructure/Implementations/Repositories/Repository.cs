using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Sarafi.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using Sarafi.Application.Interfaces.Services;

namespace Sarafi.Infrastructure.Implementations.Repositories;

public class Repository<T> : IRepository<T> where T : AggregateRoot
{
    private readonly ApplicationDbContext _context;
    private readonly long _userId;
    private readonly DateTime _now;
    /// <summary>
    /// Use the IQuerable "Query" to automatically filter by company when Inheriting from IMultitenant.
    /// Use the IQuerable "Query" to automatically filter soft-deleted entities when Inheriting from ISoftDelete.
    /// </summary>
    public IQueryable<T> Query { private set; get; }

    public Repository(ApplicationDbContext context, IUserService userService)
    {
        _context = context;
        Query = _context.Set<T>().AsQueryable();
        if (typeof(IMultiTenant).IsAssignableFrom(typeof(T)))
        {
            Query = Query.Where(e => (e as IMultiTenant).CompanyId == userService.GetCompanyId());
        }
        if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
        {
            Query = Query.Where(e => !(e as ISoftDelete).IsDeleted);
        }
        _userId = userService.GetUserId();
        _now = DateTime.Now;
    }

    public async Task<int> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(entity, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);            
    }

    public async Task<int> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        await Query.CountAsync(cancellationToken);

    public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        await Query.Where(predicate).ToListAsync(cancellationToken);

    public async Task<T?> FindByIdAsync(long Id, CancellationToken cancellationToken = default) =>
        await Query.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        await Query.FirstOrDefaultAsync<T>(predicate, cancellationToken);

    public async Task<List<T>> GetFilteredPageAsync(Expression<Func<T, bool>> predicate, int pageIndex = 0, int pageSize = 10, CancellationToken cancellationToken = default) =>
        await Query.Where(predicate).Skip(pageSize * pageIndex).Take(pageSize).ToListAsync(cancellationToken);

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await Query.ToListAsync(cancellationToken);

    public async Task<int> RemoveAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        await Query.Where(predicate).ExecuteDeleteAsync(cancellationToken);

    public async Task<int> UpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCall, CancellationToken cancellationToken = default) =>
        await Query.Where(predicate).ExecuteUpdateAsync(setPropertyCall, cancellationToken);

    public void MarkAsAdded(T entity) => _context.Entry(entity).State = EntityState.Added;

    public void MarkAsDeleted(T entity) => _context.Entry(entity).State = EntityState.Deleted;

    public void MarkAsDetached(T entity) => _context.Entry(entity).State = EntityState.Detached;

    public void MarkAsModified(T entity) => _context.Entry(entity).State = EntityState.Modified;

    public void MarkAsUnchanged(T entity) => _context.Entry(entity).State = EntityState.Unchanged;

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();
}
