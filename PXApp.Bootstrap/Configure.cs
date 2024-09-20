using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PXApp.Bootstrap;

public static class Configure
{
    public static IServiceCollection AddPXApp(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddDb(config)
            // .AddRabbitMq(config)
            .AddBusiness(config);
    }
    
    public static async Task<IHost> UsePXApp(this IHost app)
    {
        await app.Services.InitializeDb();
        
        return app;
    }

    public static async Task EnsureDatabaseIsUpToDate(this IHost app)
    {
        await app.Services.EnsureDatabaseIsUpToDate();
    }
}