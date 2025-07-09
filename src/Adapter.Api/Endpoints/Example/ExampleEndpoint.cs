using Adapter.Api.Configurations.EndpointsConfig;

namespace Adapter.Api.Endpoints.Example
{
    public class ExampleEndpoint : IEndpoint<ExampleEndpointsGroup>
    {
        public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}
