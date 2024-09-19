using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PXApp.Bootstrap;

internal static class ConfigureBusiness
{
    internal static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddSingleton<IApplicationContext, ApplicationContext>();
        //
        // services.AddScoped<IUserContext, UserContext>();
        // services.AddScoped<IUserContextHelper, UserContextHelper>();
        // services.AddScoped<IUserSecurityHelper, UserSecurityHelper>();

        return services;
    }
}