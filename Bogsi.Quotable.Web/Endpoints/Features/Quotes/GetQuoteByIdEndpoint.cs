// -----------------------------------------------------------------------
// <copyright file="GetQuoteByIdEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Handlers.Quotes;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoint to get a quote by its public id.
/// </summary>
public sealed class GetQuoteByIdEndpoint : IApiEndpoint
{
    /// <inheritdoc/>
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("quotes/{id:guid}", GetQuoteById)
            .WithTags(Constants.Endpoints.Quotes)
            .WithName(Constants.Endpoints.QuoteEndpoints.GetQuoteByIdEndpoint)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    /// <summary>
    /// Get a quote by it's public id.
    /// </summary>
    /// <param name="id">Public id of quote to delete.</param>
    /// <param name="handler">Handler for the busines logic.</param>
    /// <param name="mapper">A configured instance of AutoMapper.</param>
    /// <param name="logger">An instance of a Serilog logger.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>The quote or a status code indicating failure.</returns>
    internal static async Task<IResult> GetQuoteById(
        [FromRoute] Guid id,
        [FromServices] IGetQuoteByIdHandler handler,
        [FromServices] IMapper mapper,
        [FromServices] ILogger<GetQuoteByIdEndpoint> logger,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["endpoint"] = nameof(GetQuoteByIdEndpoint),
            ["public-id"] = id,
        });

        logger.LogInformation("[{Source}] mapping endpoint request to handler request", nameof(GetQuoteByIdEndpoint));

        GetQuoteByIdHandlerRequest handlerRequest = new ()
        {
            PublicId = id,
        };

        logger.LogInformation("[{Source}] executing handler", nameof(GetQuoteByIdEndpoint));

        var result = await handler.HandleAsync(handlerRequest, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            logger.LogError("[{Source}] something went wrong, {Error}", nameof(GetQuoteByIdEndpoint), result.Error);

            if (result.Error == QuotableErrors.NotFound)
            {
                return Results.NotFound();
            }
        }

        logger.LogInformation("[{Source}] mapping handler response to endpoint response", nameof(GetQuoteByIdEndpoint));

        var response = mapper.Map<GetQuoteByIdHandlerResponse, GetQuoteByIdResponse>(result.Value);

        return Results.Ok(response);
    }
}
