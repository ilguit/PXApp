using Microsoft.AspNetCore.Mvc;
using PXApp.API.Contracts.Request;
using PXApp.API.Contracts.Service;
using PXApp.API.Entity;
using PXApp.Common.Contracts;

namespace PXApp.API.Controllers;

public class BaseController<TTableEntity, TResponse> : ControllerBase
    where TTableEntity : class, IDbEntity, IHasId, new()
    where TResponse : class, IResponseDto, new()
{
    private const int DefaultPageSize = 15;
    
    private readonly IServiceMapper _mapper;
    private readonly IService<TTableEntity> _service;

    public BaseController(
        IServiceMapper mapper, 
        IService<TTableEntity> service)
    {
        _mapper = mapper;
        _service = service;
    }

    protected virtual async Task<ActionResult<CollectionResponse<TResponse>>> Get<TGetAllRequest>(
        TGetAllRequest request)
        where TGetAllRequest : class, IRequestAll, new()
    {
        var entities = await _service.GetAsync();

        var totalCount = await _service.CountAsync();

        var responseItems = _mapper.Map<List<TTableEntity>, List<TResponse>>(entities)!;

        return new CollectionResponse<TResponse>
        {
            Items = responseItems,
            // Count = responseItems.Count,
            // Offset = request.Skip ?? 0,
            Total = totalCount
        };
    }

    protected virtual async Task<ActionResult<TResponse>> GetById<TGetByIdRequest>(TGetByIdRequest request)
        where TGetByIdRequest : class, IRequestById, new()
    {
        var entity = await _service.GetAsync(request.Id);

        if (entity == null)
        {
            return NotFound();
        }

        return _mapper.Map<TTableEntity, TResponse>(entity)!;
    }

    protected virtual async Task<ActionResult<TResponse>> Post<TPostRequest, TPostBody>(TPostRequest request)
        where TPostRequest : class, IRequestPost<TPostBody>, new()
        where TPostBody : class, IRequestBodyDto, new()
    {
        var tableEntity = _mapper.Map<TPostBody, TTableEntity>(request.Body);
        if (tableEntity == null)
        {
            throw new NullReferenceException(nameof(tableEntity));
        }

        tableEntity = await _service.AddAsync(tableEntity);

        return _mapper.Map<TTableEntity, TResponse>(tableEntity)!;
    }

    protected virtual async Task<ActionResult<TResponse>> Put<TPutRequest, TPutBody>(TPutRequest request)
        where TPutRequest : class, IRequestPut<TPutBody>, new()
    {
        var tableEntity = await _service.GetAsync(request.Id);
        if (tableEntity == null)
        {
            return NotFound();
        }

        tableEntity = _mapper.Map(request.Body, tableEntity);
        if (tableEntity == null)
        {
            throw new NullReferenceException(nameof(tableEntity));
        }

        tableEntity = await _service.UpdateAsync(tableEntity);

        return _mapper.Map<TTableEntity, TResponse>(tableEntity)!;
    }

    protected virtual async Task<ActionResult> Delete<TDeleteRequest>(TDeleteRequest request)
        where TDeleteRequest : class, IRequestDelete, new()
    {
        await _service.DeleteAsync(request.Id);

        return Ok();
    }
}