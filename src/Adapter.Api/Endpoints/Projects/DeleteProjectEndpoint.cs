using Adapter.Api.Endpoints.Abstractions;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Projects;
public class DeleteProjectEndpoint : IEndpoint<ProjectEndpointsGroup>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapDelete("/{projectId:Guid}", DeleteProjectHandler)
                  .WithName("DeleteProject")
                  .Produces<Result>(StatusCodes.Status204NoContent)
                  .ProducesProblem(StatusCodes.Status404NotFound)
                  .WithSummary("Delete Project")
                  .WithDescription("Delete Project")
                  .RequireAuthorization();
    }

    private static async Task<Result> DeleteProjectHandler(
        [FromRoute] Guid projectId,
        [FromServices] IDeleteProject useCase,
        CancellationToken cancellationToken
    )
    {
        Result response = await useCase.ExecuteAsync(
            ProjectId.Create(projectId),
            cancellationToken
        );
        return response;
    }
}