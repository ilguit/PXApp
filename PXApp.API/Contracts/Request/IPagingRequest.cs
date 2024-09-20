namespace PXApp.API.Contracts.Request;

public interface IPagingRequest : IRequestDto
{
    public int? Take { get; set; }
    public int? Skip { get; set; }
}