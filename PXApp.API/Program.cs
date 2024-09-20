
using Microsoft.AspNetCore.Builder;
using PXApp.API;
using PXApp.API.Config;
using PXApp.Bootstrap;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfig();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPXApp(builder.Configuration)
    .AddServices()
    .AddMapping();

var app = builder.Build();

//На случай, если вдруг захочу не только по апи дёргать данные
// await app.EnsureDatabaseIsUpToDate();

app.Services.CheckMapping();

await app.UsePXApp();

// app.UseRequestContext();

app.MapControllers();

app.Run();