using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Adapter.Api.Endpoints.Users;
public class UserEndpointsConfiguration : EndpointsConfiguration
{
    public UserEndpointsConfiguration()
    {
        WithPrefix(ApiRoutes.Users);
        WithTags(ApiRoutes.Users);
    }
}