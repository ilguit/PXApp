using PXApp.API.Contracts.Request;

namespace PXApp.API.Entity.Message;

public class MessagePostBody : IRequestBodyDto
{
    // [SwaggerSchema("Текст")]
    public string Body { get; set; } = null!;
}