using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace PXApp.MauiBlazor.Config;

public static class Configure
{
    internal static MauiAppBuilder AddConfig(this MauiAppBuilder builder)
    {
        const string json = """
                            {
                                 "RabbitMqSettings": 
                                 {
                                     "HostName": "192.168.0.10",
                                     "QueueName": "PXQueue"
                                 }
                             }
                            """;
        var configuration = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json))).Build();
        
        builder.Configuration.AddConfiguration(configuration);

        return builder;
    }
}