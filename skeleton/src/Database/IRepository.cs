using System.Linq.Expressions;
using IMOv2.Api.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace IMOv2.Api.Database;

public interface IRepository<TEntity, TKey, TContext>
    where TEntity : BaseEntity<TKey>
    where TContext : DbContext
{
    public bool IsDisposed();

    Task<TEntity> GetAsync(TKey id);

    Task<List<TEntity>> GetAllAsync(int? pageNumber = null, int? pageSize = null);

    Task<List<TType>> SelectAsync<TType>(Expression<Func<TEntity, TType>> select) where TType : class;
    Task<List<TType>> SelectAsync<TType>(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TType>> select,
        params Expression<Func<TEntity, object>>[] includeProperties) where TType : class;

    Task<List<T1>> FindAsync<T1>(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task AddAsync(TEntity entity);

    Task InsertAndSaveAsync(TEntity entity);

    Task BulkInsertAndSaveAsync(List<TEntity> entities);

    void Update(TEntity entity);

    void BulkUpdate(List<TEntity> entity);

    void Delete(TEntity entity);

    Task<int>   SaveChangesAsync();

    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    Task<List<TEntity>> FindAsync(int? Offset,
                                  int? Limit,
                                  Expression<Func<TEntity, TKey>> order,
                                  Expression<Func<TEntity, bool>> predicate,
                                  params Expression<Func<TEntity, object>>[] includeProperties);

    Task<List<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);

    List<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    List<TEntity> Find(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TResult> SingleOrDefaultAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true);

    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TResult> SingleAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true);
}