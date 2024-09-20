// using Swashbuckle.AspNetCore.Annotations;

namespace PXApp.Common.Data.Entity;

public class CollectionResponse<T>
{
    public List<T> Items { get; set; } = new();
    
    public int Total { get; set; }
}