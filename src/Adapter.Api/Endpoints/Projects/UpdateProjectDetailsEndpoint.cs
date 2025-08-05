using MinimalApi.Endpoints.Organizer.Abstractions;
using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Projects
{
    public class UpdateProjectDetailsEndpoint : IEndpoint<ProjectEndpointsConfiguration>
    {
        public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
        {
            return app.MapPatch("/{projectId:Guid}", UpdateProjectDetailsHandler)
                      .WithName("UpdateProjectDetails")
                      .Produces<Result>(StatusCodes.Status200OK)
                      .ProducesProblem(StatusCodes.Status400BadRequest)
                      .ProducesProblem(StatusCodes.Status404NotFound)
                      .WithSummary("Update Project Details")
                      .WithDescription("Update Project Details")
                      .RequireAuthorization();
        }

        private static async Task<Result> UpdateProjectDetailsHandler(
            [FromRoute] Guid projectId,
            [FromBody] UpdateProjectDetailsRequestDto requestDto,
            [FromServices] IUpdateProjectDetails useCase,
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
}
