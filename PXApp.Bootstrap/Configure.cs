using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PXApp.Bootstrap;

public static class Configure
{
    public static IServiceCollection AddPXApp(this IServiceCollection services, IConfiguration config)
    {
        return services
            // .AddDb(config)
            // .AddRabbitMq(config)
            .AddBusiness(config);
    }
}