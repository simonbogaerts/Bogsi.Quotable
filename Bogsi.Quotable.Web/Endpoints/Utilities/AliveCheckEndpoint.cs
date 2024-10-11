// -----------------------------------------------------------------------
// <copyright file="AliveCheckEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Endpoints.Utilities;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;

/// <summary>
/// Alive check endpoint.
/// </summary>
internal class AliveCheckEndpoint : IApiEndpoint
{
    /// <inheritdoc/>
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("alive", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("live"),
        })
        .WithOpenApi();
    }
}
