using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace PXApp.MauiBlazor.Config;

public static class Configure
{
    internal static MauiAppBuilder AddConfig(this MauiAppBuilder builder)
    {
        //Возникли проблемы с копированием конфига в apk, так что временно такой хаодкод
        var json = """
            {
                 "RabbitMqSettings": {
                     "HostName": "192.168.0.10",
                     "QueueName": "PXQueue"
                 }
             }
            """;
        
        builder.Configuration.AddConfiguration(new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json))).Build());
        
        json = """
            {
                "ApiSettings": {
                  "BaseUri": "http://192.168.0.10:5001/api/"
                }
             }
            """;
        
        builder.Configuration.AddConfiguration(new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(json))).Build());

        return builder;
    }
}