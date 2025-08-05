using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Adapter.Api.Endpoints.Projects;
public class ProjectEndpointsConfiguration : EndpointsConfiguration
{
    public ProjectEndpointsConfiguration()
    {
        WithPrefix($"/{ApiRoutes.Projects}");
        WithTags(ApiRoutes.Projects);
    }
}