using Adapter.Api.Endpoints.Abstractions;

namespace Adapter.Api.Endpoints.Users;
public class UserEndpointsGroup : IEndpointGroup
{
    public static string GroupName => "user";

    public static string GeneralEndpointsPrefix => "user";
}