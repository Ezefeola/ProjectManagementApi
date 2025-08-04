using Adapter.Api.Endpoints.Abstractions;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Projects;
public class GetUsersForProjectEndpoint : IEndpoint<ProjectEndpointsGroup>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet("/{projectId:Guid}/users", GetUsersForProjectHandler)
                  .WithName("GetUsersForProject")
                  .Produces<Result<IEnumerable<GetUserForProjectResponseDto>>>(StatusCodes.Status200OK)
                  .ProducesProblem(StatusCodes.Status404NotFound)
                  .WithSummary("Get Users For Project")
                  .WithDescription("Get Users For Project")
                  .RequireAuthorization();
    }

    private static async Task<Result<IEnumerable<GetUserForProjectResponseDto>>> GetUsersForProjectHandler(
        [FromRoute] Guid projectId,
        [FromServices] IGetUsersForProject useCase,
        CancellationToken cancellationToken
    )
    {
        Result<IEnumerable<GetUserForProjectResponseDto>> response = await useCase.ExecuteAsync(
            ProjectId.Create(projectId),
            cancellationToken
        );
        return response;
    }
}