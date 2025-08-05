using MinimalApi.Endpoints.Organizer.Abstractions;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Projects;
public class RemoveUserFromProjectEndpoint : IEndpoint<ProjectEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapDelete("/{projectId:Guid}/user/{userId:Guid}", RemoveUserFromProjectHandler)
                  .WithName("RemoveUserFromProject")
                  .Produces<Result>(StatusCodes.Status200OK)
                  .ProducesProblem(StatusCodes.Status400BadRequest)
                  .ProducesProblem(StatusCodes.Status404NotFound)
                  .WithSummary("Remove User From Project")
                  .WithDescription("Remove User From Project")
                  .RequireAuthorization();
    }

    private static async Task<Result> RemoveUserFromProjectHandler(
        [FromRoute] Guid projectId,
        [FromRoute] Guid userId,
        [FromServices] IRemoveUserFromProject useCase,
        CancellationToken cancellationToken
    )
    {
        Result response = await useCase.ExecuteAsync(
            ProjectId.Create(projectId),
            UserId.Create(userId),
            cancellationToken
        );
        return response;
    }
}