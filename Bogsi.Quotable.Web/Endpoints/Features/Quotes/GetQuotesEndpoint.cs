// -----------------------------------------------------------------------
// <copyright file="GetQuotesEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoint to delete an existing quote.
/// </summary>
public sealed class GetQuotesEndpoint : IApiEndpoint
{
    /// <inheritdoc/>
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("quotes", GetQuotes)
            .WithTags(Constants.Endpoints.Quotes)
            .WithName(Constants.Endpoints.QuoteEndpoints.GetQuotesEndpoint)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    /// <summary>
    /// Get all quotes or filter them.
    /// </summary>
    /// <param name="parameters">Search parameters.</param>
    /// <param name="handler">Handler for the busines logic.</param>
    /// <param name="mapper">A configured instance of AutoMapper.</param>
    /// <param name="logger">An instance of a Serilog logger.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>A list containing the matching quotes and some metadata.</returns>
    internal static async Task<IResult> GetQuotes(
        [AsParameters] GetQuotesParameters parameters,
        [FromServices] IGetQuotesHandler handler,
        [FromServices] IMapper mapper,
        [FromServices] ILogger<GetQuotesEndpoint> logger,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["endpoint"] = nameof(GetQuotesEndpoint),
            ["@parameters"] = parameters,
        });

        logger.LogInformation("[{Source}] mapping parameters to handler request", nameof(GetQuotesEndpoint));

        var handlerRequest = mapper.Map<GetQuotesParameters, GetQuotesHandlerRequest>(parameters);

        logger.LogInformation("[{Source}] executing handler", nameof(GetQuotesEndpoint));

        var result = await handler.HandleAsync(handlerRequest, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            logger.LogError("[{Source}] something went wrong, {Error}", nameof(GetQuotesEndpoint), result.Error);

            return Results.Problem(statusCode: 500);
        }

        logger.LogInformation("[{Source}] mapping handler response to endpoint response", nameof(GetQuotesEndpoint));

        var response = mapper.Map<GetQuotesHandlerResponse?, GetQuotesResponse>(result.Value);

        return Results.Ok(response);
    }
}
