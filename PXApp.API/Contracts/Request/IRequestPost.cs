namespace PXApp.API.Contracts.Request;

public interface IRequestPost<T> : IRequestDto
    where T : class, IRequestBodyDto, new()
{
    public T Body { get; set; }
}