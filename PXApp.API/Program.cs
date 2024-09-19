
using Microsoft.AspNetCore.Builder;
using PXApp.API;
using PXApp.API.Config;
using PXApp.Bootstrap;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfig();

builder.Services.AddPXApp(builder.Configuration)
    .AddServices();

var app = builder.Build();

app.Run();