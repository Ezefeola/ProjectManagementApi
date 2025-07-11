﻿using System.Reflection;
using Core.Services.Encrypt;
using Core.Services.Token;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core;
public static class ServiceCollectionExtensions
{
    public static void AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidation();
        services.AddServices();
        services.AddUseCases();
    }

    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>()
                .AddScoped<IEncryptService, EncryptService>();
    }

    private static void AddUseCases(this IServiceCollection services)
    {

    }
}