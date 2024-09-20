using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PXApp.API.Contracts.Request;

// using Swashbuckle.AspNetCore.Annotations;

namespace PXApp.API.Entity.Message;

public class MessageDeleteRequest :
    IRequestDelete
{
    [Required]
    [FromRoute]
    // [SwaggerParameter(ParametersConstants.Id)]
    public Guid Id { get; set; }
}