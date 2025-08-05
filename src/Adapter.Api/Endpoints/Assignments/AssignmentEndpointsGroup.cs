using Adapter.Api.Endpoints.Abstractions;

namespace Adapter.Api.Endpoints.Assignments
{
    public class AssignmentEndpointsGroup : IEndpointGroup
    {
        public static string GroupName => ApiRoutes.Assignment;

        public static string GeneralEndpointsPrefix => ApiRoutes.Project;
    }
}
