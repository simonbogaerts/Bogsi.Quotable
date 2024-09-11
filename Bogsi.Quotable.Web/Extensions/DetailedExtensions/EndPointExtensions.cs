using Asp.Versioning.Builder;
using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Bogsi.Quotable.Web.Endpoints;

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

internal static class EndPointExtensions
{
    internal static void AddApiEndpoints(this WebApplicationBuilder builder)
    {
        var assembly = typeof(Program).Assembly;

        var serviceDescriptors = assembly
            .DefinedTypes
                .Where(type => !type.IsAbstract &&
                !type.IsInterface &&
                               type.IsAssignableTo(typeof(IApiEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IApiEndpoint), type));

        builder.Services.TryAddEnumerable(serviceDescriptors);
    }

    internal static void UseVersionedApiEndpoints(this WebApplication application)
    {
        ApiVersionSet apiVersionSet = application.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        RouteGroupBuilder versionedGroup = application
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet);

        application.RegisterApiEndpoints(versionedGroup);
    }

    internal static void RegisterApiEndpoints(this WebApplication application, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IApiEndpoint> endpoints = application.Services.GetRequiredService<IEnumerable<IApiEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null
            ? application
            : routeGroupBuilder;

        foreach (IApiEndpoint endpoint in endpoints)
        {
            endpoint.MapRoute(builder);
        }
    }
}
