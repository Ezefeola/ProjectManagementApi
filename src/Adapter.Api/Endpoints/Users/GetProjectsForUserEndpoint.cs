using Adapter.Api.Endpoints.Projects;
using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.DTOs.Projects.Response;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Adapter.Api.Endpoints.Users;
public class GetProjectsForUserEndpoint : IEndpoint<UserEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet("/{userId:Guid}"+$"/{ApiRoutes.Projects}", GetProjectsForUserHandler)
                  .WithName("GetProjectsForUser")
                  .Produces<Result<GetProjectsForUserResponseDto>>(StatusCodes.Status200OK)
                  .ProducesProblem(StatusCodes.Status404NotFound)
                  .WithSummary("Get Projects For User")
                  .WithDescription("Get Projects For User")
                  .RequireAuthorization();
    }

    private static async Task<Result<GetProjectsForUserResponseDto>> GetProjectsForUserHandler(
        [FromRoute] Guid userId,
        [AsParameters] GetProjectsForUserRequestDto parametersRequestDto,
        [FromServices] IGetProjectsForUser useCase,
        CancellationToken cancellationToken
    )
    {
        Result<GetProjectsForUserResponseDto> response = await useCase.ExecuteAsync(
            UserId.Create(userId),
            parametersRequestDto,
            cancellationToken
        );
        return response;
    }
}