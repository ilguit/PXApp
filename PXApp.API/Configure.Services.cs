using Microsoft.Extensions.DependencyInjection;
using PXApp.API.Common;
using PXApp.API.Contracts;
using PXApp.Common.RabbitMq;
using PXApp.API.Contracts.Service;
using PXApp.API.Service;
using PXApp.Common.Contracts;
using PXApp.Core.Db.Entity;

namespace PXApp.API;

public static class ConfigureServices
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRequestContext, RequestContext>();
        services.AddScoped<IRabbitMqService, RabbitMqService>();
        // services.AddHostedService<RabbitMqListener>();
        // services.AddSingleton<INotificationProvider, NotificationProvider>();

        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IService<TableMessage>, MessageService>();
        
        return services;
    }
    
}