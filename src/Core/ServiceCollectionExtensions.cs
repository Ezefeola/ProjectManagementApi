using System.Reflection;
using Core.Contracts.UseCases.Assignments;
using Core.Contracts.UseCases.Auth;
using Core.Contracts.UseCases.Projects;
using Core.Contracts.UseCases.Users;
using Core.UseCases.Assignments;
using Core.UseCases.Projects;
using Core.UseCases.Users;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core;
public static class ServiceCollectionExtensions
{
    public static void AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidation();
        services.AddUseCases();
    }

    private static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddUserUseCases();
        services.AddProjectUseCases();
        services.AddAssignmentUseCases();
    }
    private static void AddUserUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateUser, CreateUser>()
                .AddScoped<ILogin, Login>();
    }
    private static void AddProjectUseCases(this IServiceCollection services)
    {
        services.AddScoped<IGetProjects, GetProjects>()
                .AddScoped<IGetProjectById, GetProjectById>()
                .AddScoped<ICreateProject, CreateProject>()
                .AddScoped<IUpdateProjectDetails, UpdateProjectDetails>()
                .AddScoped<IDeleteProject, DeleteProject>()
                .AddScoped<IGetUsersForProject, GetUsersForProject>()
                .AddScoped<IAssignUserToProject, AssignUserToProject>()
                .AddScoped<IRemoveUserFromProject, RemoveUserFromProject>();
    }

    private static void AddAssignmentUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateAssignment, CreateAssignment>()
                .AddScoped<IUpdateAssignmentDetails, UpdateAssignmentDetails>()
                .AddScoped<IDeleteAssignment, DeleteAssignment>();
    }
}