using System.Linq.Expressions;

namespace PXApp.Common.Contracts;

public interface IServiceMapper
{
    TTo? Map<TFrom, TTo>(TFrom? entity);
    TTo Map<TFrom, TTo>(TFrom fromEntity, TTo toEntity);
    Expression<Func<TTo, bool>>? MapExpression<TFrom, TTo>(Expression<Func<TFrom, bool>>? expression);
    IQueryable<TTo> ProjectTo<TFrom, TTo>(IQueryable<TFrom> query);
}