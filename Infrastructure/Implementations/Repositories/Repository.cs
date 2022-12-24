using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Sarafi.Domain.Common;
namespace Sarafi.Infrastructure.Implementations.Repositories
{
    public class Repository<T> : IRepository<T> where T : AuditableEntity
    {
        protected readonly ApplicationDbContext context;
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
            await Task.Run(() => context.Set<T>().Count(), cancellationToken);
        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool includeSoftDeleted = true) =>
            await context.Set<T>().Where(predicate).Where(e => includeSoftDeleted || !e.IsDeleted).AsNoTracking().ToListAsync(cancellationToken);
        public async Task<T?> FindByIdAsync(long Id, CancellationToken cancellationToken = default, bool includeSoftDeleted = true)
        {
            T? entity = await context.Set<T>().FindAsync(Id, cancellationToken);
            if (entity != null && (includeSoftDeleted || !entity.IsDeleted))
                return entity;
            return null;
        }
        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, bool includeSoftDeleted = true)
        {
            T? entity = await context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
            if (entity != null && (includeSoftDeleted || entity.IsDeleted))
                return entity;
            return null;
        }
        public async Task<List<T>> GetFilteredPageAsync(Expression<Func<T, bool>> predicate, int pageIndex = 0, int pageSize = 10, CancellationToken cancellationToken = default, bool includeSoftDeleted = true) =>
            await context.Set<T>().Where(predicate).Skip(pageSize * pageIndex).Take(pageSize).AsNoTracking().ToListAsync(cancellationToken);
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default, bool includeSoftDeleted = true) =>
            await context.Set<T>().Where(e => includeSoftDeleted || !e.IsDeleted).AsNoTracking().ToListAsync(cancellationToken);
        public async Task<int> RemoveAsync(long Id, CancellationToken cancellationToken = default)
        {
            T? entity = await FindByIdAsync(Id, cancellationToken: cancellationToken);
            if (entity != null)
                await Task.Run(() => context.Set<T>().Remove(entity), cancellationToken);
            return await context.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => context.Set<T>().RemoveRange(entities), cancellationToken);
            return await context.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => context.Set<T>().Update(entity), cancellationToken);
            return await context.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => context.Set<T>().UpdateRange(entities), cancellationToken);
            return await context.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> SoftDeleteAsync(long Id, CancellationToken cancellationToken = default)
        {
            T? auditableEntiy = await context.Set<T>().FindAsync(Id, cancellationToken);
            if (auditableEntiy != null)
                auditableEntiy.IsDeleted = true;
            return await context.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> SoftDeleteRangeAsync(IEnumerable<long> Ids, CancellationToken cancellationToken = default)
        {
            List<T> entities = await FindAllAsync(e => Ids.Contains(e.Id), cancellationToken: cancellationToken);
            entities.ForEach(entity => entity.IsDeleted = true);
            return await context.SaveChangesAsync(cancellationToken);
        }
        public void MarkAsAdded(T entity) => context.Entry(entity).State = EntityState.Added;
        public void MarkAsDeleted(T entity) => context.Entry(entity).State = EntityState.Deleted;
        public void MarkAsDetached(T entity) => context.Entry(entity).State = EntityState.Detached;
        public void MarkAsModified(T entity) => context.Entry(entity).State = EntityState.Modified;
        public void MarkAsUnchanged(T entity) => context.Entry(entity).State = EntityState.Unchanged;
        public async ValueTask DisposeAsync() => await context.DisposeAsync();
    }
}
