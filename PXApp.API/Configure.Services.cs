using Microsoft.Extensions.DependencyInjection;
using PXApp.Common.Contracts;
using PXApp.Common.RabbitMq;
using PXApp.API.Common;

namespace PXApp.API;

public static class ConfigureServices
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRabbitMqService, RabbitMqService>();
        services.AddHostedService<RabbitMqListener>();
        // services.AddSingleton<INotificationProvider, NotificationProvider>();
        
        return services;
    }
    
}