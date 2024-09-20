namespace PXApp.Common.AppConfig;

public class ApiSettings
{
    public const string ConfigSection = "ApiSettings";

    public string BaseUri { get; set; } = null!;
    
}