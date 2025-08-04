using Adapter.Api.Endpoints.Abstractions;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Projects;
public class GetProjectByIdEndpoint : IEndpoint<ProjectEndpointsGroup>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet("/{projectId:Guid}", GetProjectByIdHandler);
    }

    private static async Task<Result<GetProjectByIdResponseDto>> GetProjectByIdHandler(
        [FromRoute] Guid projectId,
        [FromServices] IGetProjectById useCase,
        CancellationToken cancellationToken
    )
    {
        Result<GetProjectByIdResponseDto> response = await useCase.ExecuteAsync(
            ProjectId.Create(projectId), 
            cancellationToken
        );
        return response;
    }
}