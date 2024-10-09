// -----------------------------------------------------------------------
// <copyright file="CreateQuoteByIdEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoint to block the creation of a quote on a route with an id.
/// </summary>
public sealed class CreateQuoteByIdEndpoint : IApiEndpoint
{
    /// <inheritdoc/>
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("quotes/{id:guid}", BlockCreateQuote)
            .MapToApiVersion(1)
            .ExcludeFromDescription();
    }

    /// <summary>
    /// Endpoint logic.
    /// </summary>
    /// <param name="id">The public id of a quote.</param>
    /// <param name="repository">An instance of a readonly QuoteRepository.</param>
    /// <param name="logger">An instance of a Serilog logger.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>A specific status code.</returns>
    internal static async Task<IResult> BlockCreateQuote(
        [FromRoute] Guid id,
        [FromServices] IReadonlyRepository<Quote> repository,
        [FromServices] ILogger<CreateQuoteByIdEndpoint> logger,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["endpoint"] = nameof(CreateQuoteByIdEndpoint),
            ["public-id"] = id,
        });

        logger.LogInformation("[{Source}] checking if item exists", nameof(CreateQuoteByIdEndpoint));

        var result = await repository.ExistsAsync(id, cancellationToken).ConfigureAwait(false);

        return !result.Value
            ? Results.NotFound()
            : Results.Conflict();
    }
}
