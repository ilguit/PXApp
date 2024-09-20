using PXApp.API.Contracts.Request;

namespace PXApp.API.Entity.Message;

public class MessageResponse : IResponseDto
{
    // [SwaggerSchema(ParametersConstants.Id)]
    public Guid Id { get; set; }

    // [SwaggerSchema("Текст")]
    public string Body { get; set; } = null!;
    
    // [SwaggerSchema(ParametersConstants.DateCreated)]
    public DateTime DateCreated { get; set; }
}