using System.Text;
using Microsoft.AspNetCore.Components;
using PXApp.Common.Contracts;
using PXApp.Common.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PXApp.MauiBlazor.Components.Pages;

public partial class Counter
{
    private int _currentCount = 0;
    private string _message = string.Empty;
    
    [Inject]
    public IRabbitMqService RabbitMqService { get; set; } = null!;
    //
    // [Inject]
    // public INotificationProvider NotificationProvider { get; set; } = null!;

    protected override void OnInitialized()
    {
        // NotificationProvider.MessageReceived += message =>
        // {
        //     _message = message;
        // };
        base.OnInitialized();
    }

    private void IncrementCount()
    {
        _currentCount++;
        
        RabbitMqService.SendMessage($"counter {_currentCount}");
    }
}