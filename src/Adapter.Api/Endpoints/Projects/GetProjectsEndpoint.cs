using Adapter.Api.Endpoints.Abstractions;
using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Projects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Projects;
public class GetProjectsEndpoint : IEndpoint<ProjectEndpointsGroup>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet("/", GetProjectsHandler);
    }

    private static async Task<Result<GetProjectsResponseDto>> GetProjectsHandler(
        [FromBody] GetProjectsRequestDto parametersRequestDto,
        [FromServices] IGetProjects useCase,
        CancellationToken cancellationToken
    )
    {
        Result<GetProjectsResponseDto> response = await useCase.ExecuteAsync(parametersRequestDto, cancellationToken);
        return response;
    }
}
