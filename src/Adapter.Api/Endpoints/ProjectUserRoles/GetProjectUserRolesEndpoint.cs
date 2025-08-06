using Core.Contracts.DTOs.ProjectUserRoles.Response;
using Core.Contracts.Results;
using Core.Contracts.UseCases.ProjectUserRoles;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Adapter.Api.Endpoints.ProjectUserRoles;
public class GetProjectUserRolesEndpoint : IEndpoint<ProjectUserRoleEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet("/", GetProjectUserRolesHandler)
                  .WithName("GetProjectUserRoles")
                  .Produces<Result<GetProjectUserRolesResponseDto>>(StatusCodes.Status200OK)
                  .ProducesProblem(StatusCodes.Status404NotFound)
                  .WithSummary("Get Project User Roles")
                  .WithDescription("Get Project User Roles")
                  .RequireAuthorization();
    }

    private static async Task<Result<GetProjectUserRolesResponseDto>> GetProjectUserRolesHandler(
        [FromServices] IGetProjectUserRoles useCase,
        CancellationToken cancellationToken
    )
    {
        Result<GetProjectUserRolesResponseDto> response = await useCase.ExecuteAsync(cancellationToken);
        return response;
    }
}
