using System.Reflection;
using Core.Contracts.UseCases.Auth;
using Core.Contracts.UseCases.Projects;
using Core.Contracts.UseCases.Users;
using Core.Services.Encrypt;
using Core.Services.Token;
using Core.UseCases.Auth;
using Core.UseCases.Projects;
using Core.UseCases.Users;
using Core.Utilities.UserInfo;
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
                .AddScoped<IEncryptService, EncryptService>()
                .AddScoped<IUserInfo, UserInfo>();
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddUserUseCases();
        services.AddProjectUseCases();
    }
    private static void AddUserUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateUser, CreateUser>()
                .AddScoped<ILogin, Login>();
    }
    private static void AddProjectUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateProject, CreateProject>();
                //.AddScoped<IGetProjects, CreateAssignment>();
    }
}