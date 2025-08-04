using Adapter.Api.Endpoints.Abstractions;
using Adapter.Api.Endpoints.Projects;
using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.DTOs.Assignment.Response;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Assignments;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Assignments;
public class CreateAssignmentEndpoint : IEndpoint<ProjectEndpointsGroup>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost("/assignments", CreateAssignmentHandler)
                  .WithName("CreateAssignment")
                  .Produces<Result<CreateAssignmentResponseDto>>(StatusCodes.Status201Created)
                  .ProducesProblem(StatusCodes.Status400BadRequest)
                  .WithSummary("Create Assignment")
                  .WithDescription("Create Assignment")
                  .RequireAuthorization();
    }

    private static async Task<Result<CreateAssignmentResponseDto>> CreateAssignmentHandler(
        [FromBody] CreateAssignmentRequestDto requestDto,
        [FromServices] ICreateAssignment useCase,
        CancellationToken cancellationToken
    )
    {
        Result<CreateAssignmentResponseDto> response = await useCase.ExecuteAsync(requestDto, cancellationToken);
        return response;
    }
}