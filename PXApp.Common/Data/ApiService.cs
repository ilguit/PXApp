using Microsoft.Extensions.Configuration;

namespace PXApp.Common.Data;

public class ApiService
{
    private readonly IConfiguration _configuration;

    public ApiService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}