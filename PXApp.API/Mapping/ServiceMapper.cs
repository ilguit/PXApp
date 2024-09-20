using System.Linq.Expressions;
using AutoMapper;
using PXApp.Common.Contracts;

namespace PXApp.API.Mapping;

public class ServiceMapper : IServiceMapper
{
    private readonly IMapper _mapper;

    public ServiceMapper(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public TTo? Map<TFrom, TTo>(TFrom? entity)
    {
        return entity == null
            ? default
            : _mapper.Map<TFrom, TTo>(entity);
    }

    public TTo Map<TFrom, TTo>(TFrom fromEntity, TTo toEntity)
    {
        return _mapper.Map(fromEntity, toEntity);
    }

    public Expression<Func<TTo, bool>>? MapExpression<TFrom, TTo>(Expression<Func<TFrom, bool>>? expression)
    {
        return expression == null
            ? null
            : _mapper.Map<Expression<Func<TFrom, bool>>, Expression<Func<TTo, bool>>>(expression);
    }

    public IQueryable<TTo> ProjectTo<TFrom, TTo>(IQueryable<TFrom> query)
    {
        return _mapper.ProjectTo<TTo>(query);
    }
}