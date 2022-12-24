using Sarafi.Domain.Common;
using System.Linq.Expressions;
namespace Sarafi.Application.Interfaces.Repositories
{
    public interface IRepository<T> : IAsyncDisposable where T : AuditableEntity
    {
        Task<int> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<int> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool includeSoftDeleted = true);
        Task<List<T>> GetFilteredPageAsync(Expression<Func<T, bool>> predicate, int pageIndex = 0, int pageSize = -1, CancellationToken cancellationToken = default, bool includeSoftDeleted = true);
        Task<T?> FindByIdAsync(long Id, CancellationToken cancellationToken = default, bool includeSoftDeleted = true);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool includeSoftDeleted = true);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default, bool includeSoftDeleted = true);
        Task<int> RemoveAsync(long Id, CancellationToken cancellationToken = default);
        Task<int> RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task<int> SoftDeleteAsync(long Id, CancellationToken cancellationToken = default);
        Task<int> SoftDeleteRangeAsync(IEnumerable<long> Ids, CancellationToken cancellationToken = default);
        void MarkAsAdded(T entity);
        void MarkAsDeleted(T entity);
        void MarkAsDetached(T entity);
        void MarkAsModified(T entity);
        void MarkAsUnchanged(T entity);
    }
}
