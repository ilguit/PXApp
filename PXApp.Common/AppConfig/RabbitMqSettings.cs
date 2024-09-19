namespace PXApp.Common.AppConfig;

public class RabbitMqSettings
{
    public const string ConfigSection = "RabbitMqSettings";

    public string HostName { get; set; } = null!;
    public string QueueName { get; set; } = null!;
    
}