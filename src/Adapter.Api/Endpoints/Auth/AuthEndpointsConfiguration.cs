using MinimalApi.Endpoints.Organizer.Abstractions;

namespace Adapter.Api.Endpoints.Auth;
public class AuthEndpointsConfiguration : EndpointsConfiguration
{
    public AuthEndpointsConfiguration()
    {
        WithPrefix($"/{ApiRoutes.Auth}");
        WithTags(ApiRoutes.Auth);
    }
}