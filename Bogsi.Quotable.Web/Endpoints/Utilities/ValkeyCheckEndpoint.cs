// -----------------------------------------------------------------------
// <copyright file="ValkeyCheckEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Endpoints.Utilities;

using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;

/// <summary>
/// Valkey health check endpoint.
/// Test endpoint used furing development.
/// </summary>
internal class ValkeyCheckEndpoint : IApiEndpoint
{
    /// <inheritdoc/>
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("utilities/valkey-health", ValkeyHealthCheck)
            .WithTags(Constants.Endpoints.Utilities)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    /// <summary>
    /// Endpoint logic.
    /// </summary>
    /// <param name="distributedCache">Instance of Valkey.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Random gibberish from Valkey.</returns>
    internal static async Task<IResult> ValkeyHealthCheck(
        IDistributedCache distributedCache,
        CancellationToken cancellationToken)
    {
        bool isFound = distributedCache is not null;

        if (!isFound)
        {
            Results.Problem(statusCode: 500);
        }

        string key = "valkey-test";

        await distributedCache!
            .SetStringAsync(key, JsonSerializer.Serialize("hello, Valkey"), cancellationToken)
            .ConfigureAwait(false);

        var result = await distributedCache!
            .GetStringAsync(key, cancellationToken)
            .ConfigureAwait(false);

        return !string.IsNullOrWhiteSpace(result)
            ? Results.Ok(result)
            : Results.Problem(statusCode: 500);
    }
}
