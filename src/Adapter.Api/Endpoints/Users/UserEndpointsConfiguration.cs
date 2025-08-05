using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Adapter.Api.Endpoints.Users;
public class UserEndpointsConfiguration : EndpointsConfiguration
{
    public UserEndpointsConfiguration()
    {
        WithPrefix("/user");
        WithTags("user");
    }
}