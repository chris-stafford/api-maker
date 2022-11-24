using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using IMOv2.Api.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace IMOv2.Api.Database;
public class GenericRepository<TEntity, TKey, TContext>
    : IRepository<TEntity, TKey, TContext>, IDisposable
        where TEntity : BaseEntity<TKey>
        where TContext : DbContext
{
    private readonly DbSet<TEntity> DbSet;
    private readonly TContext Context;
    protected readonly IMapper Mapper;
    protected readonly ILogger<GenericRepository<TEntity, TKey, TContext>> Logger;
    private bool Disposed;

    public bool IsDisposed()
    {
        return Disposed;
    }

    public GenericRepository(
        TContext context,
        IMapper mapper,
        ILogger<GenericRepository<TEntity, TKey, TContext>> logger)
    {
        this.Context = context;
        this.DbSet = this.Context.Set<TEntity>();
        this.Mapper = mapper;
        this.Logger = logger;
    }

    public virtual async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await this.DbSet.Where(predicate).AsNoTracking().ToListAsync();
    }

    public virtual async Task<List<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = GetAllIncluding(includeProperties);
        var results = await query.AsNoTracking().Where(predicate).ToListAsync();
        return results;
    }

    public virtual async Task<List<TEntity>> FindAsync(
        int? Offset,
        int? Limit,
        Expression<Func<TEntity, TKey>> order,
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {

        if (Limit < 0 || Offset < 0)
            throw new ArgumentOutOfRangeException($"Invalid Page Size of {Limit}");

        var query = GetAllIncluding(includeProperties);

        if (!Limit.HasValue || Limit == 0) return await query.AsNoTracking().Where(predicate).ToListAsync();

        if (Offset.HasValue)
        {
            var skip = Offset * Limit;
            return await query.AsNoTracking().Where(predicate).OrderBy(order).Skip(skip.Value).Take(Limit.Value).ToListAsync();
        }

        return null;
    }

    public virtual async Task<List<T1>> FindAsync<T1>(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = GetAllIncluding(includeProperties);
        return await query
            .AsNoTracking()
            .Where(predicate)
            .ProjectTo<T1>(this.Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public virtual async Task<TEntity> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var queryable = this.DbSet.AsQueryable();
        var query = includeProperties.Aggregate(queryable,
            (current, includeProperty) => current.Include(includeProperty));
        return await query.FirstOrDefaultAsync(predicate);
    }

    public virtual List<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return this.DbSet.Where(predicate).AsNoTracking().ToList();
    }

    public virtual List<TEntity> Find(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = GetAllIncluding(includeProperties);
        var results = query.Where(predicate).AsNoTracking().ToList();
        return results;
    }

    public virtual async Task<List<TType>> SelectAsync<TType>(Expression<Func<TEntity, TType>> select) where TType : class
    {
        return await this.DbSet.AsNoTracking().Select(select).ToListAsync();
    }

    public virtual async Task<List<TType>> SelectAsync<TType>(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TType>> select,
        params Expression<Func<TEntity, object>>[] includeProperties) where TType : class
    {
        var query = GetAllIncluding(includeProperties);
        var results = await query.Where(predicate).AsNoTracking().Select(select).ToListAsync();
        return results;
    }

    public virtual async Task<List<TEntity>> GetAllAsync(int? pageNumber = null, int? pageSize = null)
    {
        if (pageSize < 0 || pageNumber < 0)
            throw new ArgumentOutOfRangeException($"Invalid Page Size of {pageSize}");
        var dbSet = this.DbSet.AsNoTracking();
        if (!pageSize.HasValue || pageSize == 0) return await dbSet.ToListAsync();
        if (pageNumber.HasValue)
        {
            var skip = (pageNumber - 1) * pageSize;
            dbSet = dbSet.Skip(skip.Value);
        }
        var take = pageSize;
        dbSet = dbSet.Take(take.Value);
        return await dbSet.ToListAsync();
    }

    public virtual async Task<TEntity> GetAsync(TKey id)
    {
        return await this.DbSet.SingleOrDefaultAsync(e => e.Id.Equals(id));
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await this.DbSet.AddAsync(entity);
    }

    public virtual async Task InsertAndSaveAsync(TEntity entity)
    {
        await this.DbSet.AddAsync(entity);
        await this.SaveChangesAsync();
    }

    public virtual async Task BulkInsertAndSaveAsync(List<TEntity> entities)
    {
        await this.DbSet.AddRangeAsync(entities);
        await this.SaveChangesAsync();
    }

    public virtual void Update(TEntity entity)
    {
        this.DbSet.Update(entity);
    }

    public virtual void BulkUpdate(List<TEntity> entity)
    {
        this.DbSet.UpdateRange(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        this.DbSet.Remove(entity);
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        try
        {
            return await this.Context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (ex is InvalidOperationException)
            {
                this.Logger.LogError(ex, "SaveChangesAsync");
            }

            throw;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.Disposed)
        {
            if (disposing)
            {
                this.Context.Dispose();
            }
        }

        this.Disposed = true;
    }

    private IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var queryable = this.DbSet.AsNoTracking();
        return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
    }

    public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = GetAllIncluding(includeProperties);
        var result = await query.AsNoTracking().SingleOrDefaultAsync(predicate);
        return result;
    }

    public async Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = this.DbSet.AsQueryable();
        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            return await orderBy(query).Select(selector).SingleOrDefaultAsync();
        }
        else
        {
            return await query.Select(selector).SingleOrDefaultAsync();
        }
    }

    public virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = GetAllIncluding(includeProperties);
        var result = await query.AsNoTracking().SingleAsync(predicate);
        return result;
    }

    public virtual async Task<TResult> SingleAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true)
    {
        IQueryable<TEntity> query = this.DbSet.AsQueryable();
        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            return await orderBy(query).Select(selector).SingleAsync();
        }
        else
        {
            return await query.Select(selector).SingleAsync();
        }
    }
}