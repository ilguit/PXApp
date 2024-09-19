using Microsoft.Extensions.Configuration;
using PXApp.Common.AppConfig;
using PXApp.Common.Contracts;

namespace PXApp.Common.RabbitMq;

using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Diagnostics;
using System;


public class RabbitMqListener : BackgroundService
{
    private readonly IConfiguration _configuration;
    private IConnection? _connection;
    private IModel? _channel;
    private readonly INotificationProvider _notificationProvider;
    private  RabbitMqSettings? _rabbitMqSettings;

    public RabbitMqListener(IConfiguration configuration, INotificationProvider notificationProvider)
    {
        _configuration = configuration;
        _notificationProvider = notificationProvider;
        InitRabbitMq();
    }

    private void InitRabbitMq()
    {
        _rabbitMqSettings = _configuration.GetSection(RabbitMqSettings.ConfigSection).Get<RabbitMqSettings>();

        if (string.IsNullOrEmpty(_rabbitMqSettings?.HostName))
        {
            throw new InvalidOperationException("RabbitMQ HostName is not set");
        }
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqSettings.HostName,
            // Port = rabbitMqSettings.Port,
            // UserName = rabbitMqSettings.UserName,
            // Password = rabbitMqSettings.Password
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _rabbitMqSettings.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			
            _notificationProvider.OnMessageReceived(content);

            _channel?.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(_rabbitMqSettings?.QueueName, false, consumer);

        return Task.CompletedTask;
    }
	
    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}