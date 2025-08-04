using Adapter.Api.Endpoints.Abstractions;
using Core.Contracts.DTOs.Projects.Request;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Projects
{
    public class UpdateProjectDetailsEndpoint : IEndpoint<ProjectEndpointsGroup>
    {
        public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
        {
            return app.MapPatch("/{projectId:Guid}", UpdateProjectDetailsHandler);
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
