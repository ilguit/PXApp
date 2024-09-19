namespace PXApp.Web.Config;

public static class Configure
{
    internal static WebApplicationBuilder AddConfig(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        
        const string configurationsDirectory = "Config";
            
        var env = builder.Environment;
        
        config
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            // .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/rabbitmq.json", optional: false, reloadOnChange: true)
            // .AddJsonFile($"{configurationsDirectory}/database.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder;
    }
}