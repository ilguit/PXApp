using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PXApp.API.Contracts;
using PXApp.API.Contracts.Service;
using PXApp.Common.Contracts;
using PXApp.Common.Entity;
using PXApp.Core.Db;

namespace PXApp.API.Service;

public class BaseService<TTableEntity> : IService<TTableEntity>
    where TTableEntity : class, IDbEntity, IHasId, new()
{
    private readonly IDbContextFactory<PXAppDbContext> _dbContextFactory;
    private readonly IRequestContext _requestContext;
    private readonly IContextFilter<TTableEntity>? _contextFilter;

    public BaseService(IDbContextFactory<PXAppDbContext> dbContextFactory,
        IRequestContext requestContext,
        IContextFilter<TTableEntity>? contextFilter = null)
    {
        _dbContextFactory = dbContextFactory;
        _requestContext = requestContext;
        _contextFilter = contextFilter;
    }
    
    public Expression<Func<TTableEntity, bool>>? GetContextFilter()
    {
        return _contextFilter?.Filter();
    }

    public async Task<List<TTableEntity>> GetAsync(
        Expression<Func<TTableEntity, bool>>? expression = null,
        PagingParams? pagingParams = null)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        var query = db.Set<TTableEntity>().AsQueryable();
        
        if (expression != null)
        {
            query = query.Where(expression);
        }

        if (typeof(TTableEntity).IsAssignableTo(typeof(IHasDateCreated)))
        {
            query = query.OrderByDescending(x => ((IHasDateCreated)x).DateCreated);
        }
        
        var entities = await query.ToListAsync();

        return entities;
    }

    public async Task<TTableEntity?> GetAsync(Guid id)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        var query = db
            .Set<TTableEntity>()
            .Where(x => x.Id == id);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync(Expression<Func<TTableEntity, bool>>? expression = null)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        var query = db.Set<TTableEntity>().AsQueryable();
        
        if (expression != null)
        {
            query = query.Where(expression);
        }

        return await query.CountAsync();
    }

    public async Task<TTableEntity> AddAsync(TTableEntity entity)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        if (entity is IHasDateCreated entityDateCreated &&
            entityDateCreated.DateCreated == DateTime.MinValue)
        {
            entityDateCreated.DateCreated = DateTime.UtcNow;
        }
        
        await db.AddAsync(entity);

        await db.SaveChangesAsync();

        return entity;
    }

    public async Task<TTableEntity> UpdateAsync(TTableEntity entity)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        db.Update(entity);

        await db.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        var query = db
            .Set<TTableEntity>()
            .Where(x => x.Id == id);

        var entity = await query.FirstOrDefaultAsync();

        switch (entity)
        {
            case null:
                return;

            default:
                db.Remove(entity);
                break;
        }

        await db.SaveChangesAsync();
    }
}