using Adapter.Api.Endpoints.Abstractions;
using Adapter.Api.Endpoints.Projects;
using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Assignments;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Assignments;
public class UpdateAssignmentDetailsEndpoint : IEndpoint<ProjectEndpointsGroup>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPatch("/{projectId:Guid}/assignments/{assignmentId:Guid}", UpdateAssignmentDetailsHandler)
                  .WithName("UpdateAssignmentDetails")
                  .Produces<Result>(StatusCodes.Status204NoContent)
                  .ProducesProblem(StatusCodes.Status400BadRequest)
                  .ProducesProblem(StatusCodes.Status404NotFound)
                  .WithSummary("Update Assignment Details")
                  .WithDescription("Update Assignment Details")
                  .RequireAuthorization();
    }

    private static async Task<Result> UpdateAssignmentDetailsHandler(
        [FromRoute] Guid projectId,
        [FromRoute] Guid assignmentId,
        [FromBody] UpdateAssignmentDetailsRequestDto requestDto,
        [FromServices] IUpdateAssignmentDetails useCase,
        CancellationToken cancellationToken
    )
    {
        Result response = await useCase.ExecuteAsync(
            ProjectId.Create(projectId),
            AssignmentId.Create(assignmentId),
            requestDto,
            cancellationToken
        );
        return response;
    }
}