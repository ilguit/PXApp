using Microsoft.AspNetCore.Components;
using PXApp.Common.Contracts;
using PXApp.Common.RabbitMq;

namespace PXApp.Web.Components.Pages;

public partial class Counter
{
    private int _currentCount = 0;
    private string _message = string.Empty;
    
    [Inject]
    public IRabbitMqService RabbitMqService { get; set; } = null!;

    [Inject]
    public INotificationProvider NotificationProvider { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        NotificationProvider.MessageReceived += message =>
        {
            _message = message;
            Refresh();
            
        };
        await base.OnInitializedAsync();
    }

    private void Refresh()
    {
        InvokeAsync(StateHasChanged);
    }

    private void IncrementCount()
    {
        _currentCount++;
        
        RabbitMqService.SendMessage($"counter {_currentCount}");
    }
}