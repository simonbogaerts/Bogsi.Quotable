// -----------------------------------------------------------------------
// <copyright file="UpdateQuoteEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Handlers.Quotes;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoint to create a new quote.
/// </summary>
public sealed class UpdateQuoteEndpoint : IApiEndpoint
{
    /// <inheritdoc/>
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPut("quotes/{id:guid}", UpdateQuote)
            .WithTags(Constants.Endpoints.Quotes)
            .WithName(Constants.Endpoints.QuoteEndpoints.UpdateQuoteEndpoint)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    /// <summary>
    /// Update ane existing quote.
    /// </summary>
    /// <param name="id">Public id of quote to delete.</param>
    /// <param name="request">Incoming request body.</param>
    /// <param name="mediator">Handler for the busines logic.</param>
    /// <param name="validator">Validator for the incoming request.</param>
    /// <param name="mapper">A configured instance of AutoMapper.</param>
    /// <param name="logger">An instance of a Serilog logger.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>A statuscode indicating success or failure.</returns>
    internal static async Task<IResult> UpdateQuote(
        [FromRoute] Guid id,
        [FromBody] UpdateQuoteRequest request,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<UpdateQuoteCommand> validator,
        [FromServices] IMapper mapper,
        [FromServices] ILogger<UpdateQuoteEndpoint> logger,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["endpoint"] = nameof(UpdateQuoteEndpoint),
            ["public-id"] = id,
        });

        logger.LogInformation("[{Source}] mapping endpoint request to handler request", nameof(UpdateQuoteEndpoint));

        var handlerRequest = mapper.Map<UpdateQuoteRequest, UpdateQuoteCommand>(request, opt => opt.Items["Id"] = id);

        logger.LogInformation("[{Source}] validating handler request", nameof(UpdateQuoteEndpoint));

        var isValidRequest = await validator.ValidateAsync(handlerRequest, cancellationToken).ConfigureAwait(false);

        if (!isValidRequest.IsValid)
        {
            logger.LogError("[{Source}] invalid handler request", nameof(UpdateQuoteEndpoint));

            return Results.ValidationProblem(isValidRequest.ToDictionary());
        }

        logger.LogInformation("[{Source}] executing handler", nameof(UpdateQuoteEndpoint));

        var result = await mediator.Send(handlerRequest, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            logger.LogError("[{Source}] something went wrong, {Error}", nameof(CreateQuoteEndpoint), result.Error);

            if (result.Error == QuotableErrors.NotFound)
            {
                return Results.NotFound();
            }

            if (result.Error == QuotableErrors.InternalError)
            {
                return Results.Problem(statusCode: 500);
            }
        }

        return Results.NoContent();
    }
}
