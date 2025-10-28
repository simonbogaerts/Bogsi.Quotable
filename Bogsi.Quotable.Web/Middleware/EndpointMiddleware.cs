// -----------------------------------------------------------------------
// <copyright file="EndpointMiddleware.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Middleware;

using Asp.Versioning;
using Asp.Versioning.Builder;

using Bogsi.Quotable.Web.Endpoints;

/// <summary>
/// Middleware regarding endpoints.
/// </summary>
internal static class EndpointMiddleware
{
    /// <summary>
    /// Add endpoints to configured version.
    /// </summary>
    /// <param name="application">WebApplication.</param>
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

    /// <summary>
    /// Add discovered endpints to endpoint collection.
    /// </summary>
    /// <param name="application">WebApplication.</param>
    /// <param name="routeGroupBuilder">Routegroupbuilder to configure routes.</param>
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
