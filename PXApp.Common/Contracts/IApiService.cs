namespace PXApp.Common.Contracts;

public interface IApiService<TEntity>
    where TEntity : class, IServiceEntity, IHasId, new()
{
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity> GetAsync(Guid id);
    Task<List<TEntity>> GetAsync();
}