namespace PXApp.Common.Contracts;

public interface INotificationProvider
{
    // public void Subscribe(IScopedNotificationProvider scopedNotificationProvider);
    // public void Unsubscribe(string circuitId);
    public Task OnMessageReceived(string message);
    
    public event MessageReceivedHandler? MessageReceived;
}