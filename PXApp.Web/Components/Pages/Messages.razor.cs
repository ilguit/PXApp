using Microsoft.AspNetCore.Components;
using PXApp.Common.Contracts;
using PXApp.Common.RabbitMq;
using System.Net.Http.Headers;
using PXApp.Common.Data;
using PXApp.Common.Data.Entity;

namespace PXApp.Web.Components.Pages;

public partial class Messages
{
    private List<Message> _messages = new();
    private string _messageBody = string.Empty;
    
    [Inject]
    public IRabbitMqService RabbitMqService { get; set; } = null!;

    [Inject]
    public INotificationProvider NotificationProvider { get; set; } = null!;
    
    [Inject]
    public MessageService MessageService { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        NotificationProvider.MessageReceived += message =>
        {
            // _message = message;
            InvokeAsync(Refresh);
            
        };
        
        await UpdateMessages();
        
        await base.OnInitializedAsync();
    }

    private async Task UpdateMessages()
    {
        _messages = await GetMessages();
    }

    private async Task<List<Message>> GetMessages()
    {
        return await MessageService.GetAsync();
    }

    private async Task Refresh()
    {
        await UpdateMessages();
        StateHasChanged();
    }

    private async Task OnSendMessageClick()
    {
        //TODO: надо валидировать на фронте
        if (string.IsNullOrEmpty(_messageBody)) return;
        
        await SendMessage(_messageBody);
        RabbitMqService.SendMessage(_messageBody);
    }

    private async Task SendMessage(string body)
    {
        await MessageService.AddAsync(new Message(body));
    }

    private async Task OnDeleteMessageClick(Message message)
    {
        await DeleteMessage(message);
        RabbitMqService.SendMessage("Удалено");
    }

    private async Task DeleteMessage(Message message)
    {
        await MessageService.DeleteAsync(message);
    }
}