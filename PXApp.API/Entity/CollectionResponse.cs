// using Swashbuckle.AspNetCore.Annotations;

namespace PXApp.API.Entity;

public class CollectionResponse<T>
{
    // [SwaggerSchema("Коллекция объектов")]
    public List<T> Items { get; set; } = new();

    // [SwaggerSchema("Количество возвращенных объектов")]
    // public int Count { get; set; }
    
    // [SwaggerSchema("Количество пропущенных объектов")]
    // public int Offset { get; set; }
    
    // [SwaggerSchema("Общее количество доступных объектов")]
    public int Total { get; set; }
}