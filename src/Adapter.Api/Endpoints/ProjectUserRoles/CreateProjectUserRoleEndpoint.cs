using Core.Contracts.DTOs.ProjectUserRoles.Request;
using Core.Contracts.Results;
using Core.Contracts.UseCases.ProjectUserRoles;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Adapter.Api.Endpoints.ProjectUserRoles;
public class CreateProjectUserRoleEndpoint : IEndpoint<ProjectUserRoleEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost("/", CreateProjectUserRoleHandler)
                  .WithName("CreateProjectUserRoles")
                  .Produces<Result>(StatusCodes.Status201Created)
                  .ProducesProblem(StatusCodes.Status400BadRequest)
                  .WithSummary("Create Project User Roles")
                  .WithDescription("Create Project User Roles")
                  .RequireAuthorization();
    }

    private static async Task<Result> CreateProjectUserRoleHandler(
        [FromBody] CreateProjectUserRoleRequestDto requestDto,
        [FromServices] ICreateProjectUserRole useCase,
        CancellationToken cancellationToken
    )
    {
        Result response = await useCase.ExecuteAsync(requestDto, cancellationToken);
        return response;
    }
}