namespace PXApp.API.Contracts.Request;

public interface IRequestPut<T> : IRequestById
{
    public T Body { get; set; }
}