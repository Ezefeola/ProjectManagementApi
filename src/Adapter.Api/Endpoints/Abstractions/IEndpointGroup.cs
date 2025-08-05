namespace Adapter.Api.Endpoints.Abstractions;
public interface IEndpointGroup
{
    public static abstract string GroupName { get; }
    public static abstract string GeneralEndpointsPrefix { get; }
}