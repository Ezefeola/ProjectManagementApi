using Adapter.Api.Endpoints.Abstractions;

namespace Adapter.Api.Endpoints.Auth;
public class AuthEndpointsGroup : IEndpointGroup
{
    public static string GroupName => "auth";

    public static string GeneralEndpointsPrefix => "auth";
}