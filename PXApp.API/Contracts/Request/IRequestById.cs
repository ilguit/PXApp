namespace PXApp.API.Contracts.Request;

public interface IRequestById : IRequestDto
{
    public Guid Id { get; set; }
}