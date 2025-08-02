using Adapter.Api.Endpoints.Abstractions;
using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.DTOs.Users.Response;
using Core.Contracts.Result;
using Core.Contracts.UseCases.Projects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Projects;
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
                  .RequireAuthorization(); ;
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