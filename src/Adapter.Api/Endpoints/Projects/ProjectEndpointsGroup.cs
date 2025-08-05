using Adapter.Api.Endpoints.Abstractions;

namespace Adapter.Api.Endpoints.Projects;
public class ProjectEndpointsGroup : IEndpointGroup
{
    public static string GroupName => ApiRoutes.Project;

    public static string GeneralEndpointsPrefix => ApiRoutes.Project;
}