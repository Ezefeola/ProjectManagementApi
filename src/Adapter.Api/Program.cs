using Adapter.Api.Extensions;
using Adapter.SqlServer.Data.Seeders;
using CompositionRoot;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddApiConfig(builder.Configuration);

builder.Services.AddCompositionRoot(builder.Configuration);

#endregion Services

var app = builder.Build();

#region Middlewares

if(app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.AddApiWebApplicationConfig();

#endregion Middlewares

await app.RunAsync();