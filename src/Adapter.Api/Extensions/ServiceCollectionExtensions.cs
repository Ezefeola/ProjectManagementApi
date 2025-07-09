﻿using Adapter.Api.Configurations.OpenApiConfig;
using Adapter.Api.ExceptionHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Adapter.Api.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddApiConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtAuthenticationConfig(configuration);
        services.AddApiHealtChecksConfig();
        services.AddOpenApiConfig();
        services.AddCustomExceptionHandlerConfig();
        services.AddApiAuthorizationConfig();
        services.AddHttpContextAccessorConfig();
    }

    private static void AddHttpContextAccessorConfig(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
    }

    private static void AddJwtAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        string? issuer = configuration["Jwt:Issuer"];
        string? audience = configuration["Jwt:Audience"];
        string? key = configuration["Jwt:Key"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
            };
        });
    }

    private static void AddOpenApiConfig(this IServiceCollection services)
    {
        services.AddOpenApi("v1", options => { options.AddDocumentTransformer<OpenApiSecuritySchemeConfig>(); });
    }

    private static void AddCustomExceptionHandlerConfig(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
    }

    private static void AddApiHealtChecksConfig(this IServiceCollection services)
    {
        services.AddHealthChecks()
                        .AddCheck("Api Health", () => HealthCheckResult.Healthy("Api is healthy"));
    }

    private static void AddApiAuthorizationConfig(this IServiceCollection services)
    {
        services.AddAuthentication();
        services.AddAuthorization();
    }
}