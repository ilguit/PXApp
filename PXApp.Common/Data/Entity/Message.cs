using PXApp.Common.Contracts;

namespace PXApp.Common.Data.Entity;

public class Message :
    IServiceEntity,
    IHasId,
    IHasDateCreated
{
    public Message(){}
    public Message(string body)
    {
        this.Body = body;
    }
    public Guid Id { get; set; }
    public string Body { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
}