using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Adapter.Api.Endpoints.ProjectUserRoles;
public class ProjectUserRoleEndpointsConfiguration : EndpointsConfiguration
{
    public ProjectUserRoleEndpointsConfiguration()
    {
        WithPrefix(ApiRoutes.ProjectUserRoles);
        WithTags(nameof(ApiRoutes.ProjectUserRoles));
    }
}