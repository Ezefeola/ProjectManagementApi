using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Adapter.Api.Endpoints.Assignments;
public class AssignmentEndpointsConfiguration : EndpointsConfiguration
{
    public AssignmentEndpointsConfiguration()
    {
        WithPrefix($"/{ApiRoutes.Projects}" + "/{projectId:Guid}" + $"/{ApiRoutes.Assignments}");
        WithTags(ApiRoutes.Assignments);
    }
}
