using PXApp.Common.Contracts;

namespace PXApp.Core.Db.Entity;

public class TableMessage :
    IDbEntity,
    IHasId,
    IHasDateCreated
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string Body { get; set; } = string.Empty;
}