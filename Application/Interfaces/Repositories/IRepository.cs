using Sarafi.Domain.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Sarafi.Application.Interfaces.Repositories;

public interface IRepository<T> : IAsyncDisposable where T : AuditableEntity
{
    IQueryable<T> Query { get; }

    Task<int> AddAsync(T entity, CancellationToken cancellationToken = default);

    Task<int> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool includeSoftDeleted = false);

    Task<T?> FindByIdAsync(long Id, CancellationToken cancellationToken = default, bool includeSoftDeleted = false);

    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool includeSoftDeleted = false);

    Task<List<T>> GetFilteredPageAsync(Expression<Func<T, bool>> predicate, int pageIndex = 0, int pageSize = 10, CancellationToken cancellationToken = default, bool includeSoftDeleted = false);

    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default, bool includeSoftDeleted = false);

    Task<int> RemoveAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    public Task<int> UpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCall, CancellationToken cancellationToken = default);

    Task<int> SoftDeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    void MarkAsAdded(T entity);

    void MarkAsDeleted(T entity);

    void MarkAsDetached(T entity);

    void MarkAsModified(T entity);

    void MarkAsUnchanged(T entity);
}