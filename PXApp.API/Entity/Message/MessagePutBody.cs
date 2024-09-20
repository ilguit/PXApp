using PXApp.API.Contracts.Request;

namespace PXApp.API.Entity.Message;

public class MessagePutBody : IRequestBodyDto
{
    // [SwaggerSchema("Текст")]
    public string Body { get; set; } = null!;
}