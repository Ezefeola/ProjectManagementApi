using Core.Contracts.DTOs.Assignment.Request;
using Core.Contracts.DTOs.Assignment.Response;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Assignments;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Adapter.Api.Endpoints.Assignments;
public class GetAssignmentsForProjectEndpoint : IEndpoint<AssignmentEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet("/", GetAssignmentsForProjectHandler);
    }

    private static async Task<Result<GetAssignmentsForProjectResponseDto>> GetAssignmentsForProjectHandler(
        [FromRoute] Guid projectId,
        [AsParameters] GetAssignmentsForProjectRequestDto parametersRequestDto,
        [FromServices] IGetAssignmentsForProject useCase,
        CancellationToken cancellationToken
    )
    {
        Result<GetAssignmentsForProjectResponseDto> response = await useCase.ExecuteAsync(
            ProjectId.Create(projectId),
            parametersRequestDto,
            cancellationToken
        );
        return response;
    }
}