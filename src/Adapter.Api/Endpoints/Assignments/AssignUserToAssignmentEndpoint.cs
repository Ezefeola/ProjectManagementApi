using MinimalApi.Endpoints.Organizer.Abstractions;
using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Assignments;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Assignments;
public class AssignUserToAssignmentEndpoint : IEndpoint<AssignmentEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost("/{assignmentId:Guid}" + $"/{ApiRoutes.Users}", AssignUserToAssignmentsHandler)
                  .WithName("AssignUserToAssignment")
                  .Produces<Result>(StatusCodes.Status200OK)
                  .ProducesProblem(StatusCodes.Status400BadRequest)
                  .ProducesProblem(StatusCodes.Status404NotFound)
                  .WithSummary("Assign User To Assignment")
                  .WithDescription("Assign User To Assignment")
                  .RequireAuthorization();
    }

    private static async Task<Result> AssignUserToAssignmentsHandler(
        [FromRoute] Guid projectId,
        [FromRoute] Guid assignmentId,
        [FromBody] AssignUserToAssignmentRequestDto requestDto,
        [FromServices] IAssignUserToAssignment useCase,
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