using System.Reflection;
using Adapter.Api.Endpoints.Abstractions;

namespace Adapter.Api.Extensions;
public static class EndpointExtensions
{
    public static void MapEndpoints(
        this IEndpointRouteBuilder builder,
        Assembly assembly,
        Action<EndpointRegistrationOptions>? configure = null
    )
    {
        EndpointRegistrationOptions? options = new();
        configure?.Invoke(options);
        List<Type>? endpointTypes = FindEndpoints(assembly);

        foreach (Type? endpointType in endpointTypes)
        {
            RegisterEndpoint(builder, endpointType, options);
        }
    }

    private static List<Type> FindEndpoints(Assembly assembly)
    {
        return assembly.GetTypes()
                       .Where(x =>
                            x.IsClass &&
                            !x.IsAbstract &&
                            x.GetConstructor(Type.EmptyTypes) != null &&
                            x.GetInterfaces()
                                .Any(i =>
                                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEndpoint<>
                                ))
                       ).ToList();
    }

    private static void RegisterEndpoint(
        IEndpointRouteBuilder builder,
        Type endpointType,
        EndpointRegistrationOptions? endpointRegistrationOptions
    )
    {
        Type? endpointInterface = endpointType.GetInterfaces()
                                                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEndpoint<>));

        Type? groupType = endpointInterface.GetGenericArguments()[0];

        PropertyInfo? groupNameProperty = groupType.GetProperty(nameof(IEndpointGroup.GroupName), BindingFlags.Public | BindingFlags.Static);
        string? groupName = (string?)groupNameProperty?.GetValue(null) ?? groupType.Name.ToLower();

        PropertyInfo? generalEndpointPrefix = groupType.GetProperty(nameof(IEndpointGroup.GeneralEndpointsPrefix), BindingFlags.Public | BindingFlags.Static);
        string? endpointPrefix = (string?)generalEndpointPrefix?.GetValue(null) ?? groupType.Name.ToLower();

        string prefix = $"{endpointRegistrationOptions?.BasePrefix}/{endpointPrefix}".TrimStart('/').TrimEnd('/');

        RouteGroupBuilder routeGroup = builder.MapGroup(prefix)
                                              .WithTags(groupName)
                                              .WithOpenApi();
        endpointRegistrationOptions?.ConfigureGroup?.Invoke(routeGroup);

        object? endpointInstance = Activator.CreateInstance(endpointType);

        MethodInfo? mapMethod = endpointInterface.GetMethod("MapEndpoint");

        mapMethod?.Invoke(endpointInstance, [routeGroup]);
    }
}

public class EndpointRegistrationOptions
{
    public string? BasePrefix { get; set; }
    public Action<RouteGroupBuilder>? ConfigureGroup { get; set; }
}