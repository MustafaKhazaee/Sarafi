using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Sarafi.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using Sarafi.Application.Interfaces.Services;

namespace Sarafi.Infrastructure.Implementations.Repositories;

public class Repository<T> : IRepository<T> where T : AuditableEntity
{
    private readonly ApplicationDbContext _context;
    private readonly long _userId;
    private readonly DateTime _now;
    /// <summary>
    /// Use the IQuerable "Query" to automatically filter companies.
    /// </summary>
    public IQueryable<T> Query { private set; get; }

    public Repository(ApplicationDbContext context, IUserService userSeervice)
    {
        _context = context;
        Query = _context.Set<T>().Where(e => e.CompanyId == userSeervice.GetCompanyId()).AsQueryable();
        _userId = userSeervice.GetUserId();
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

    public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool includeSoftDeleted = false) =>
        await Query.Where(e => includeSoftDeleted || !e.IsDeleted).Where(predicate).AsNoTracking().ToListAsync(cancellationToken);

    public async Task<T?> FindByIdAsync(long Id, CancellationToken cancellationToken = default, bool includeSoftDeleted = false) =>
        await Query.AsNoTracking().FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool includeSoftDeleted = false) =>
        await Query.AsNoTracking().FirstOrDefaultAsync<T>(predicate, cancellationToken);

    public async Task<List<T>> GetFilteredPageAsync(Expression<Func<T, bool>> predicate, int pageIndex = 0, int pageSize = 10, CancellationToken cancellationToken = default, bool includeSoftDeleted = false) =>
        await Query.Where(predicate).Skip(pageSize * pageIndex).Take(pageSize).AsNoTracking().ToListAsync(cancellationToken);

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default, bool includeSoftDeleted = false) =>
        await Query.Where(e => includeSoftDeleted || !e.IsDeleted).AsNoTracking().ToListAsync(cancellationToken);

    public async Task<int> RemoveAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        await Query.Where(predicate).ExecuteDeleteAsync(cancellationToken);

    public async Task<int> UpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCall, CancellationToken cancellationToken = default) =>
        await Query.Where(predicate).ExecuteUpdateAsync(setPropertyCall, cancellationToken);

    public async Task<int> SoftDeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
        await UpdateAsync(predicate, e => e.SetProperty(e => e.IsDeleted, d => true)
                                           .SetProperty(e => e.DeletedById, e => _userId)
                                           .SetProperty(e => e.DeletedDate, e => _now)
        , cancellationToken);

    public void MarkAsAdded(T entity) => _context.Entry(entity).State = EntityState.Added;

    public void MarkAsDeleted(T entity) => _context.Entry(entity).State = EntityState.Deleted;

    public void MarkAsDetached(T entity) => _context.Entry(entity).State = EntityState.Detached;

    public void MarkAsModified(T entity) => _context.Entry(entity).State = EntityState.Modified;

    public void MarkAsUnchanged(T entity) => _context.Entry(entity).State = EntityState.Unchanged;

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();
}
