namespace PXApp.Common.AppConfig;

public class DatabaseSettings
{
    public const string ConfigSection = "DatabaseSettings";

    public string Endpoint { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Database { get; set; } = null!;

    public string GetConnectionString()
    {
        return
            $"Server={Endpoint};User Id={UserId};Password={Password};Database={Database};Pooling=true;MinPoolSize=0;MaxPoolSize=200;Include Error Detail=true;";
    }
}