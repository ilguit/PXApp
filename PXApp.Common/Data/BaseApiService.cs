using System.Net.Http.Headers;
using Flurl;
using Microsoft.Extensions.Configuration;
using PXApp.Common.AppConfig;
using PXApp.Common.Contracts;
using PXApp.Common.Data.Entity;

namespace PXApp.Common.Data;

public class BaseApiService<TEntity> :IApiService<TEntity>
    where TEntity : class, IServiceEntity, IHasId, new()
{
    private readonly string _path;

    static HttpClient _client = new ();

    public BaseApiService(IConfiguration configuration, string path)
    {
        var apiSettings = configuration.GetSection(ApiSettings.ConfigSection).Get<ApiSettings>();
        _path = apiSettings?.BaseUri + path;
        
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<TEntity> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TEntity>> GetAsync()
    {
        var messages = new CollectionResponse<TEntity>();
        var response = await _client.GetAsync(_path);
        if (response.IsSuccessStatusCode)
        {
            messages = await response.Content.ReadAsAsync<CollectionResponse<TEntity>>();
        }
        return messages.Items;
    }

    public async Task AddAsync(TEntity entity)
    {
        var response = await _client.PostAsJsonAsync(
            _path, entity);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        var uri = Url.Combine(_path, $"{entity.Id}");
        var response = await _client.DeleteAsync(uri);
        
        response.EnsureSuccessStatusCode();
    }
}