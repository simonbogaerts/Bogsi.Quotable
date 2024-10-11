// -----------------------------------------------------------------------
// <copyright file="FlushCacheEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using StackExchange.Redis;

namespace Bogsi.Quotable.Web.Endpoints.Utilities;

/// <summary>
/// Endpoint to flush all cache.
/// </summary>
internal class FlushCacheEndpoint : IApiEndpoint
{
    /// <inheritdoc/>
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("utilities/flush-cache", FlushCache)
            .WithTags(Constants.Endpoints.Utilities)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    /// <summary>
    /// Endpoint logic.
    /// </summary>
    /// <param name="muxer">Valkey multiplexer api implementation.</param>
    /// <returns>Some gibberish I put into Valkey.</returns>
    internal static async Task<IResult> FlushCache(IConnectionMultiplexer muxer)
    {
        await muxer.GetServers()[0].FlushAllDatabasesAsync().ConfigureAwait(false);

        return Results.Ok();
    }
}
