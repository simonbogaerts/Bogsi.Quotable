// -----------------------------------------------------------------------
// <copyright file="IApiEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bogsi.Quotable.Web.Endpoints;

/// <summary>
/// Interface used during route creation.
/// </summary>
public interface IApiEndpoint
{
    /// <summary>
    /// Provide a discoverable endpoint. During startup these are found through reflection.
    /// </summary>
    /// <param name="endpoints">Endpoint collection.</param>
    void MapRoute(IEndpointRouteBuilder endpoints);
}

/// <summary>
/// Extensions regarding endpoints.
/// </summary>
internal static class EndPointExtensions
{
    /// <summary>
    /// Scan assembly endpoints.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void ConfigureApiEndpoints(this WebApplicationBuilder builder)
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
}
