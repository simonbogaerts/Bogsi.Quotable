﻿// -----------------------------------------------------------------------
// <copyright file="CreateQuoteEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Handlers.Quotes;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoint to create a new quote.
/// </summary>
public sealed class CreateQuoteEndpoint : IApiEndpoint
{
    /// <inheritdoc/>
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("quotes", CreateQuote)
            .WithTags(Constants.Endpoints.Quotes)
            .WithName(Constants.Endpoints.QuoteEndpoints.CreateQuoteEndpoint)
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status409Conflict)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    /// <summary>
    /// Endpoint logic.
    /// </summary>
    /// <param name="request">Incoming request body.</param>
    /// <param name="handler">Handler for the busines logic.</param>
    /// <param name="validator">Validator for the incoming request.</param>
    /// <param name="mapper">A configured instance of AutoMapper.</param>
    /// <param name="logger">An instance of a Serilog logger.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Newly created quote.</returns>
    internal static async Task<IResult> CreateQuote(
        [FromBody] CreateQuoteRequest request,
        [FromServices] ICreateQuoteHandler handler,
        [FromServices] IValidator<CreateQuoteHandlerRequest> validator,
        [FromServices] IMapper mapper,
        [FromServices] ILogger<CreateQuoteEndpoint> logger,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["endpoint"] = nameof(CreateQuoteEndpoint),
        });

        logger.LogInformation("[{Source}] mapping endpoint request to handler request", nameof(CreateQuoteEndpoint));

        var handlerRequest = mapper.Map<CreateQuoteRequest, CreateQuoteHandlerRequest>(request);

        logger.LogInformation("[{Source}] validating handler request", nameof(CreateQuoteEndpoint));

        var isValidRequest = await validator.ValidateAsync(handlerRequest, cancellationToken).ConfigureAwait(false);

        if (!isValidRequest.IsValid)
        {
            logger.LogError("[{Source}] invalid handler request", nameof(CreateQuoteEndpoint));

            return Results.ValidationProblem(isValidRequest.ToDictionary());
        }

        logger.LogInformation("[{Source}] executing handler", nameof(CreateQuoteEndpoint));

        var result = await handler.HandleAsync(handlerRequest, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            logger.LogError("[{Source}] something went wrong, {Error}", nameof(CreateQuoteEndpoint), result.Error);

            if (result.Error == QuotableErrors.InternalError)
            {
                return Results.Problem(statusCode: 500);
            }
        }

        logger.LogInformation("[{Source}] mapping handler response to endpoint response", nameof(CreateQuoteEndpoint));

        var response = mapper.Map<CreateQuoteHandlerResponse, CreateQuoteResponse>(result.Value);

        return Results.CreatedAtRoute(
            Constants.Endpoints.QuoteEndpoints.GetQuoteByIdEndpoint,
            new { id = response.PublicId },
            response);
    }
}
