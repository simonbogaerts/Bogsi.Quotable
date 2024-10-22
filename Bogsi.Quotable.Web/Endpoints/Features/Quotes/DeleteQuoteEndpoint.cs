// -----------------------------------------------------------------------
// <copyright file="DeleteQuoteEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Handlers.Quotes;

using MediatR;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoint to delete an existing quote.
/// </summary>
public sealed class DeleteQuoteEndpoint : IApiEndpoint
{
    /// <inheritdoc/>
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapDelete("quotes/{id:guid}", DeleteQuote)
            .WithTags(Common.Constants.Endpoint.EndpointGroups.Quotes)
            .WithName(Common.Constants.Endpoint.QuoteEndpoints.DeleteQuoteEndpoint)
            .Produces(StatusCodes.Status202Accepted)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    /// <summary>
    /// Delete an existing quote by its public id.
    /// </summary>
    /// <param name="id">Public id of quote to delete.</param>
    /// <param name="mediator">Handler for the busines logic.</param>
    /// <param name="logger">An instance of a Serilog logger.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Status code indicating succes or failure.</returns>
    internal static async Task<IResult> DeleteQuote(
        [FromRoute] Guid id,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<DeleteQuoteEndpoint> logger,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["endpoint"] = nameof(DeleteQuoteEndpoint),
            ["public-id"] = id,
        });

        logger.LogInformation("[{Source}] mapping endpoint request to handler request", nameof(DeleteQuoteEndpoint));

        DeleteQuoteCommand request = new ()
        {
            PublicId = id,
        };

        logger.LogInformation("[{Source}] executing handler", nameof(DeleteQuoteEndpoint));

        var result = await mediator.Send(request, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            logger.LogError("[{Source}] something went wrong, {Error}", nameof(DeleteQuoteEndpoint), result.Error);

            if (result.Error == QuotableErrors.NotFound)
            {
                return Results.NotFound();
            }

            if (result.Error == QuotableErrors.InternalError)
            {
                return Results.Problem(statusCode: 500);
            }
        }

        return Results.Accepted();
    }
}
