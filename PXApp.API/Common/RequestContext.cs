using PXApp.API.Contracts;

namespace PXApp.API.Common;

public class RequestContext : IRequestContext
{
    public Guid? Id { get; set; }
}