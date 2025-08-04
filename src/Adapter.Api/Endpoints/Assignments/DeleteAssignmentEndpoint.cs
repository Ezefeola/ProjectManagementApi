using Adapter.Api.Endpoints.Abstractions;
using Adapter.Api.Endpoints.Projects;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Assignments;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Assignments;
public class DeleteAssignmentEndpoint : IEndpoint<ProjectEndpointsGroup>
{
    private readonly string route = "{projectId:Guid}"+$"/+{ApiRoutes.Projects.Assignments}"+"/{assignmentId:Guid}";
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapDelete(route, DeleteAssignmentHandler)
                  .WithName("DeleteAssignment")
                  .Produces<Result>(StatusCodes.Status200OK)
                  .ProducesProblem(StatusCodes.Status400BadRequest)
                  .ProducesProblem(StatusCodes.Status404NotFound)
                  .WithSummary("Delete Assignment")
                  .WithDescription("Delete Assignment")
                  .RequireAuthorization();
    }

    private static async Task<Result> DeleteAssignmentHandler(
        [FromRoute] Guid projectId,
        [FromRoute] Guid assignmentId,
        [FromServices] IDeleteAssignment useCase,
        CancellationToken cancellationToken
    )
    {
        Result response = await useCase.ExecuteAsync(
            ProjectId.Create(projectId),
            AssignmentId.Create(assignmentId), 
            cancellationToken
        );
        return response;
    }
}