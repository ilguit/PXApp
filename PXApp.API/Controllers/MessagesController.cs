using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PXApp.API.Contracts.Service;
using PXApp.API.Entity;
using PXApp.API.Entity.Message;
using PXApp.Common.Contracts;
using PXApp.Core.Db.Entity;

namespace PXApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class MessagesController : BaseController<TableMessage, MessageResponse>
{
    public MessagesController(IServiceMapper mapper, IService<TableMessage> service) : base(mapper, service)
    {
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
        return await base.Post<MessagePostRequest, MessagePostBody>(request);
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
        return await base.Delete(request);
    }
}