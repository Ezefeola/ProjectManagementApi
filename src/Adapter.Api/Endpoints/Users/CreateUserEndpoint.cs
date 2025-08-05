using MinimalApi.Endpoints.Organizer.Abstractions;
using Core.Contracts.DTOs.Users.Request;
using Core.Contracts.DTOs.Users.Response;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Users;
using Core.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Users;
public class CreateUserEndpoint : IEndpoint<UserEndpointsConfiguration>
{
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapPost("/", CreateUserHandler)
                  .WithName("CreateUser")
                  .Produces<Result<CreateUserResponseDto>>(StatusCodes.Status201Created)
                  .ProducesProblem(StatusCodes.Status400BadRequest)
                  .ProducesProblem(StatusCodes.Status401Unauthorized)
                  .ProducesProblem(StatusCodes.Status403Forbidden)
                  .WithSummary("Create User")
                  .WithDescription("Create User")
                  .RequireAuthorization(User.AuthorizationPolicies.RequireAdmin);
    }

    private static async Task<Result<CreateUserResponseDto>> CreateUserHandler(
        [FromBody] CreateUserRequestDto requestDto,
        [FromServices] ICreateUser useCase,
        CancellationToken cancellationToken
    )
    {
        Result<CreateUserResponseDto> response = await useCase.ExecuteAsync(requestDto, cancellationToken);
        return response;
    }
}