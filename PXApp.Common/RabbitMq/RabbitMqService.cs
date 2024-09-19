using Microsoft.Extensions.Configuration;
using PXApp.Common.AppConfig;

namespace PXApp.Common.RabbitMq;

using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMqService : IRabbitMqService
{
    private readonly IConfiguration _configuration;

    public RabbitMqService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        SendMessage(message);
    }

    public void SendMessage(string message)
    {
        var rabbitMqSettings = _configuration.GetSection(RabbitMqSettings.ConfigSection).Get<RabbitMqSettings>();

        if (string.IsNullOrEmpty(rabbitMqSettings?.HostName))
        {
            throw new InvalidOperationException("RabbitMQ HostName is not set");
        }
        var factory = new ConnectionFactory
        {
            HostName = rabbitMqSettings.HostName,
            // Port = rabbitMqSettings.Port,
            // UserName = rabbitMqSettings.UserName,
            // Password = rabbitMqSettings.Password
        };
        using var connection = factory.CreateConnection();
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: rabbitMqSettings.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                routingKey: rabbitMqSettings.QueueName,
                basicProperties: null,
                body: body);
        }
    }
}