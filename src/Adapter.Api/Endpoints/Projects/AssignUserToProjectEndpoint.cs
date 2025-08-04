using Adapter.Api.Endpoints.Abstractions;
using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Projects;
public class AssignUserToProjectEndpoint : IEndpoint<ProjectEndpointsGroup>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost("/{projectId:Guid}/users", AssignUserToProjectHandler);
    }

    private static async Task<Result> AssignUserToProjectHandler(
        [FromRoute] Guid projectId,
        [FromBody] AssignUserToProjectRequestDto requestDto,
        [FromServices] IAssignUserToProject useCase,
        CancellationToken cancellationToken
    )
    {
        Result response = await useCase.ExecuteAsync(
            ProjectId.Create(projectId),
            requestDto, 
            cancellationToken
        );
        return response;
    }
}