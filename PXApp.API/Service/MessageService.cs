using Microsoft.EntityFrameworkCore;
using PXApp.API.Contracts;
using PXApp.API.Contracts.Service;
using PXApp.Core.Db;
using PXApp.Core.Db.Entity;

namespace PXApp.API.Service;

public class MessageService : BaseService<TableMessage>, IMessageService
{
    public MessageService(IDbContextFactory<PXAppDbContext> dbContextFactory,
        IRequestContext requestContext,
        IContextFilter<TableMessage>? contextFilter = null) : base(dbContextFactory,
        requestContext, contextFilter)
    {
    }
}