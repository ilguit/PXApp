using PXApp.Common.Contracts;

namespace PXApp.Common.Data.Entity;

public class Message(string body) :
    IServiceEntity,
    IHasId,
    IHasDateCreated
{
    public Guid Id { get; set; }
    public string Body { get; set; } = body;
    public DateTime DateCreated { get; set; }
}