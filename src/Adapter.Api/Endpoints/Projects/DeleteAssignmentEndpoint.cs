using Adapter.Api.Endpoints.Abstractions;
using Core.Contracts.Results;
using Core.Contracts.UseCases.Projects;
using Core.Domain.Projects.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Endpoints.Projects;
public class DeleteAssignmentEndpoint : IEndpoint<ProjectEndpointsGroup>
{
    private readonly string route = "{projectId:Guid}"+$"/+{ApiRoutes.Projects.Project}"+"/{assignmentId:Guid}";
    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapDelete(route, DeleteAssignmentHandler);
    }

    private static async Task<Result> DeleteAssignmentHandler(
        [FromRoute] Guid projectId,
        [FromRoute] Guid assignmentId,
        [FromServices] IDeleteAssignment useCase,
        CancellationToken cancellationToken
    )
    {
        Result response = await useCase.ExecuteAsync(
            ProjectId.Create(projectId),
            AssignmentId.Create(assignmentId), 
            cancellationToken
        );
        return response;
    }
}