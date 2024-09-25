using Microsoft.AspNetCore.Mvc;
using PXApp.API.Contracts.Request;

namespace PXApp.API.Entity.Message;

public class MessageGetRequest :
    IRequestAll
{
    [FromQuery]
    // [SwaggerParameter(ParametersConstants.Take)]
    public int? Take { get; set; }

    [FromQuery]
    // [SwaggerParameter(ParametersConstants.Skip)]
    public int? Skip { get; set; }
}