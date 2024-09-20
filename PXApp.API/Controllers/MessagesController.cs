using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PXApp.API.Contracts.Service;
using PXApp.API.Entity;
using PXApp.API.Entity.Message;
using PXApp.Common.Contracts;
using PXApp.Common.RabbitMq;
using PXApp.Core.Db.Entity;

namespace PXApp.API.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
// [Authorize]
public class MessagesController : BaseController<TableMessage, MessageResponse>
{
    private IRabbitMqService _rabbitMqService { get; set; }

    public MessagesController(IServiceMapper mapper,
        IService<TableMessage> service,
        IRabbitMqService rabbitMqService) : base(mapper, service)
    {
        _rabbitMqService = rabbitMqService;
    }

    [HttpGet]
    // [SwaggerOperation("Получение списка сообщений")]
    public async Task<ActionResult<CollectionResponse<MessageResponse>>> Get(MessageGetRequest request)
    {
        return await base.Get(request);
    }

    [HttpGet("{Id:guid}")]
    // [SwaggerOperation("Получение сообщения")]
    public async Task<ActionResult<MessageResponse>> GetById(MessageGetByIdRequest request)
    {
        return await base.GetById(request);
    }

    [HttpPost]
    // [SwaggerOperation("Создание сообщения")]
    public async Task<ActionResult<MessageResponse>> Post(MessagePostRequest request)
    {
        var result = await base.Post<MessagePostRequest, MessagePostBody>(request);
        
        if (result.Value?.Body != null) _rabbitMqService.SendMessage(result.Value.Body);
        
        return result;
    }

    [HttpPut("{Id:guid}")]
    // [SwaggerOperation("Обновление сообщения")]
    public async Task<ActionResult<MessageResponse>> Put(MessagePutRequest request)
    {
        return await base.Put<MessagePutRequest, MessagePutBody>(request);
    }

    [HttpDelete("{Id:guid}")]
    // [SwaggerOperation("Удаление сообщения")]
    public async Task<ActionResult> Delete(MessageDeleteRequest request)
    {
        var result = await base.Delete(request);
        _rabbitMqService.SendMessage("Сообщение удалено");
        return result;
    }
}