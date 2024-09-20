using System.Linq.Expressions;
using PXApp.Common.Contracts;
using PXApp.Common.Entity;

namespace PXApp.API.Contracts.Service;

public interface IService<TTableEntity>
    where TTableEntity : class, IDbEntity, IHasId, new()
{   
    Expression<Func<TTableEntity, bool>>? GetContextFilter();
    
    Task<List<TTableEntity>> GetAsync(Expression<Func<TTableEntity, bool>>? expression = null,
        PagingParams? pagingParams = null);

    Task<TTableEntity?> GetAsync(Guid id);
    Task<int> CountAsync(Expression<Func<TTableEntity, bool>>? expression = null);
    Task<TTableEntity> AddAsync(TTableEntity entity);
    Task<TTableEntity> UpdateAsync(TTableEntity entity);
    Task DeleteAsync(Guid id);
}