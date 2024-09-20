using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace PXApp.API.Config;

public static class Configure
{
    internal static WebApplicationBuilder AddConfig(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        
        const string configurationsDirectory = "Config";
            
        config
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/rabbitmq.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/database.json", optional: true,
                reloadOnChange: true);

        return builder;
    }
}