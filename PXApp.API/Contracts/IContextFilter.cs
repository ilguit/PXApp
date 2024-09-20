using System.Linq.Expressions;
using PXApp.Common.Contracts;

namespace PXApp.API.Contracts;

public interface IContextFilter<TTableEntity>
    where TTableEntity : class, IDbEntity, IHasId, new()
{
    Expression<Func<TTableEntity, bool>> Filter();
}