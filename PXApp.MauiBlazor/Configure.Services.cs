using PXApp.Common.Contracts;
using PXApp.Common.RabbitMq;
using PXApp.MauiBlazor.Common;

namespace PXApp.MauiBlazor;

public static class ConfigureServices
{
    internal static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IRabbitMqService, RabbitMqService>();
        //BackgroundService не работает в Андроиде
        // services.AddHostedService<RabbitMqListener>();
        // services.AddSingleton<INotificationProvider, NotificationProvider>();
        
        return services;
    }
    
}