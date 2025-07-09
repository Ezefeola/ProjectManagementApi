using Adapter.Api.Extensions;
using CompositionRoot;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddApiConfig(builder.Configuration);

builder.Services.AddCompositionRoot(builder.Configuration);

#endregion Services

var app = builder.Build();

#region Middlewares

app.AddApiWebApplicationConfig();

#endregion Middlewares

await app.RunAsync();