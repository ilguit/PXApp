using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using PXApp.Common.Contracts;
using PXApp.Common.Data.Entity;
using PXApp.Common.RabbitMq;

namespace PXApp.MauiBlazor.Components.Pages;

public partial class Messages
{
    private List<Message> _messages = new();
    private string _messageBody = string.Empty;
    private const string ApiPath = "/messages";
    private const string ApiBaseAddr = "http://192.168.0.10:5001/api/";

    static HttpClient _client = new();

    [Inject]
    public IRabbitMqService RabbitMqService { get; set; } = null!;

    // [Inject]
    // public INotificationProvider NotificationProvider { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        // NotificationProvider.MessageReceived += message =>
        // {
        //     // _message = message;
        //     InvokeAsync(Refresh);
        //     
        // };

        //TODO: временное решение
        if (_client.BaseAddress == null)
        {
            _client.BaseAddress = new Uri(ApiBaseAddr);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
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
        try
        {
            //Пришлось в манифесте добавлять android:usesCleartextTraffic="true",
            //но это так себе решение
            var response = await _client.GetAsync(ApiPath);
            if (response.IsSuccessStatusCode)
            {
                messages = await response.Content.ReadAsAsync<CollectionResponse<Message>>();
            }
        }
        catch (Exception)
        {
            // ignored
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
        var response = await _client.PostAsJsonAsync(
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
        var response = await _client.DeleteAsync($"{ApiPath}/{message.Id}");

        response.EnsureSuccessStatusCode();
    }
}