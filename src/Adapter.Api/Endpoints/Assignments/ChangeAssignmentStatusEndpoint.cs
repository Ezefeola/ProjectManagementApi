using Adapter.Api.Endpoints.Abstractions;
using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.DTOs.Assignment.Response;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Assignments;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Assignments;
public class ChangeAssignmentStatusEndpoint : IEndpoint<AssignmentEndpointsGroup>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPatch("/{projectId:Guid}/assignment/{assignmentId:Guid}/status", ChangeAssignmentStatusHandler)
                  .WithName("ChangeAssignmentStatus")
                  .Produces<Result<CreateAssignmentResponseDto>>(StatusCodes.Status204NoContent)
                  .ProducesProblem(StatusCodes.Status400BadRequest)
                  .ProducesProblem(StatusCodes.Status404NotFound)
                  .WithSummary("Change Assignment Status")
                  .WithDescription("Change Assignment Status")
                  .RequireAuthorization();
    }

    private static async Task<Result> ChangeAssignmentStatusHandler(
        [FromRoute] Guid projectId,
        [FromRoute] Guid assignmentId,
        [FromBody] ChangeAssignmentStatusRequestDto requestDto,
        [FromServices] IChangeAssignmentStatus useCase,
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