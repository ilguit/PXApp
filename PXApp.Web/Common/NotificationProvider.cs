using PXApp.Common;
using PXApp.Common.Contracts;

namespace PXApp.Web.Common;

public class NotificationProvider : INotificationProvider
{
    public async Task OnMessageReceived(string message)
    {
        await Task.Yield();

        _ = Task.Run(() =>
        {
            MessageReceived?.Invoke(message);
        });
    }
    
    public event MessageReceivedHandler? MessageReceived;
}