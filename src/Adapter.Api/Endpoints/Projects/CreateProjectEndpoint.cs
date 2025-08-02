using Adapter.Api.Endpoints.Abstractions;
using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.DTOs.Users.Response;
using Core.Contracts.Result;
using Core.Contracts.UseCases.Projects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Projects;
public class CreateProjectEndpoint : IEndpoint<ProjectEndpointsGroup>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost("/", CreateProjectHandler)
                  .WithName("CreateProject")
                  .Produces<Result<CreateProjectResponseDto>>(StatusCodes.Status201Created)
                  .ProducesProblem(StatusCodes.Status400BadRequest)
                  .WithSummary("Create Project")
                  .WithDescription("Create Project")
                  .RequireAuthorization();
    }

    private static async Task<Result<CreateProjectResponseDto>> CreateProjectHandler(
        [FromBody] CreateProjectRequestDto requestDto,
        [FromServices] ICreateProject useCase,
        CancellationToken cancellationToken
    )
    {
        Result<CreateProjectResponseDto> response = await useCase.ExecuteAsync(requestDto, cancellationToken);
        return response;
    }
}