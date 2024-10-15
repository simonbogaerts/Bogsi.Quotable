// -----------------------------------------------------------------------
// <copyright file="GetQuotesEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes;

using MediatR;

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
            .Produces(StatusCodes.Status500InternalServerError)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    /// <summary>
    /// Get all quotes or filter them.
    /// </summary>
    /// <param name="parameters">Search parameters.</param>
    /// <param name="mediator">Handler for the busines logic.</param>
    /// <param name="mapper">A configured instance of AutoMapper.</param>
    /// <param name="logger">An instance of a Serilog logger.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>A list containing the matching quotes and some metadata.</returns>
    internal static async Task<IResult> GetQuotes(
        [AsParameters] GetQuotesParameters parameters,
        [FromServices] IMediator mediator,
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

        var handlerRequest = mapper.Map<GetQuotesParameters, GetQuotesQuery>(parameters);

        logger.LogInformation("[{Source}] executing handler", nameof(GetQuotesEndpoint));

        var result = await mediator.Send(handlerRequest, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            logger.LogError("[{Source}] something went wrong, {Error}", nameof(GetQuotesEndpoint), result.Error);

            return Results.Problem(statusCode: 500);
        }

        logger.LogInformation("[{Source}] mapping handler response to endpoint response", nameof(GetQuotesEndpoint));

        var response = mapper.Map<Application.Handlers.Quotes.GetQuotesResponse, Application.Contracts.Quotes.GetQuotesResponse>(result.Value);

        return Results.Ok(response);
    }
}
