using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Handlers.Quotes;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

public sealed class CreateQuoteEndpoint : IApiEndpoint
{
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
            ["endpoint"] = nameof(CreateQuoteEndpoint)
        });

        logger.LogInformation("[{source}] mapping endpoint request to handler request", nameof(CreateQuoteEndpoint));

        var handlerRequest = mapper.Map<CreateQuoteRequest, CreateQuoteHandlerRequest>(request);

        logger.LogInformation("[{source}] validating handler request", nameof(CreateQuoteEndpoint));

        var isValidRequest = await validator.ValidateAsync(handlerRequest, cancellationToken);

        if (!isValidRequest.IsValid)
        {
            logger.LogError("[{source}] invalid handler request", nameof(CreateQuoteEndpoint));

            return Results.ValidationProblem(isValidRequest.ToDictionary());
        }

        logger.LogInformation("[{source}] executing handler", nameof(CreateQuoteEndpoint));

        var result = await handler.HandleAsync(handlerRequest, cancellationToken);

        if (result.IsFailure)
        {
            logger.LogError("[{source}] something went wrong, {error}", nameof(CreateQuoteEndpoint), result.Error);

            if (result.Error == QuotableErrors.InternalError)
            {
                return Results.Problem(statusCode: 500);
            }
        }

        logger.LogInformation("[{source}] mapping handler response to endpoint response", nameof(CreateQuoteEndpoint));

        var response = mapper.Map<CreateQuoteHandlerResponse, CreateQuoteResponse>(result.Value);
        
        return Results.CreatedAtRoute(
            Constants.Endpoints.QuoteEndpoints.GetQuoteByIdEndpoint,
            new { id = response.PublicId }, 
            response);
    }
}
