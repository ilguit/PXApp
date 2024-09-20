using Microsoft.AspNetCore.Components;
using PXApp.Common.Contracts;
using PXApp.Common.RabbitMq;
using System.Net.Http.Headers;
using PXApp.Common.Data.Entity;

namespace PXApp.Web.Components.Pages;

public partial class Messages
{
    private List<Message> _messages = new();
    private string _messageBody = string.Empty;
    private const string ApiPath = "/api/messages";
    private const string ApiBaseAddr = "http://192.168.0.10:5001/";
    
    static HttpClient client = new ();
    
    [Inject]
    public IRabbitMqService RabbitMqService { get; set; } = null!;

    [Inject]
    public INotificationProvider NotificationProvider { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        NotificationProvider.MessageReceived += message =>
        {
            // _message = message;
            InvokeAsync(Refresh);
            
        };
        
        //TODO: временное решение
        if(client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(ApiBaseAddr);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        await UpdateMessages();
        
        await base.OnInitializedAsync();
    }

    private async Task UpdateMessages()
    {
        _messages = await GetMessages();
    }

    private async Task<List<Message>> GetMessages()
    {
        var messages = new CollectionResponse<Message>();
        var response = await client.GetAsync(ApiPath);
        if (response.IsSuccessStatusCode)
        {
            messages = await response.Content.ReadAsAsync<CollectionResponse<Message>>();
        }
        return messages.Items;
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
        var message = new Message(body);
        var response = await client.PostAsJsonAsync(
            ApiPath, message);
        response.EnsureSuccessStatusCode();
    }

    private async Task OnDeleteMessageClick(Message message)
    {
        await DeleteMessage(message);
        RabbitMqService.SendMessage("Удалено");
    }

    private async Task DeleteMessage(Message message)
    {
        var response = await client.DeleteAsync($"{ApiPath}/{message.Id}");
        
        response.EnsureSuccessStatusCode();
    }
}