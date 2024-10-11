// -----------------------------------------------------------------------
// <copyright file="HealthCheckEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Bogsi.Quotable.Web.Endpoints.Utilities;

/// <summary>
/// Health check endpoint.
/// </summary>
internal class HealthCheckEndpoint : IApiEndpoint
{
    /// <inheritdoc/>
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("healthz", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        })
        .WithOpenApi();
    }
}
