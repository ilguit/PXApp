using Microsoft.Extensions.Configuration;
using PXApp.Common.AppConfig;

namespace PXApp.Common.RabbitMq;

using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMqService : IRabbitMqService
{
    private readonly string _queueName;
    private readonly IModel _channel;

    public RabbitMqService(IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection(RabbitMqSettings.ConfigSection).Get<RabbitMqSettings>();
        
        var hostName = rabbitMqSettings?.HostName ?? string.Empty;
        _queueName = rabbitMqSettings?.QueueName ?? string.Empty;

        if (string.IsNullOrEmpty(hostName))
        {
            throw new InvalidOperationException("RabbitMQ HostName is not set");
        }
        
        var factory = new ConnectionFactory
        {
            HostName = hostName,
            // Port = rabbitMqSettings.Port,
            // UserName = rabbitMqSettings.UserName,
            // Password = rabbitMqSettings.Password
        };
        var connection = factory.CreateConnection();
        
        _channel = connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }
    public void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        SendMessage(message);
    }

    public void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: string.Empty,
            routingKey: _queueName,
            basicProperties: null,
            body: body);
    }
}