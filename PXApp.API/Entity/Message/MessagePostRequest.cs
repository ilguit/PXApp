using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PXApp.API.Contracts.Request;

namespace PXApp.API.Entity.Message;

public class MessagePostRequest :
    IRequestPost<MessagePostBody>
{
    [Required]
    [FromBody]
    public MessagePostBody Body { get; set; } = null!;
}