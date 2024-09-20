using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PXApp.API.Contracts.Request;

namespace PXApp.API.Entity.Message;

public class MessagePutRequest :
    IRequestPut<MessagePutBody>
{
    [Required]
    [FromRoute]
    // [SwaggerParameter(ParametersConstants.Id)]
    public Guid Id { get; set; }
    
    [Required]
    [FromBody]
    public MessagePutBody Body { get; set; } = null!;
}