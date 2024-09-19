using PXApp.Common.Contracts;
using PXApp.Common.RabbitMq;
using PXApp.Web.Common;

namespace PXApp.Web;

public static class ConfigureServices
{
    internal static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IRabbitMqService, RabbitMqService>();
        services.AddHostedService<RabbitMqListener>();
        services.AddSingleton<INotificationProvider, NotificationProvider>();
        
        return services;
    }
    
}