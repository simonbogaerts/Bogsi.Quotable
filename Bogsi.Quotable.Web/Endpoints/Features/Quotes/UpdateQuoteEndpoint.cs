using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Handlers.Quotes;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

public sealed class UpdateQuoteEndpoint : IApiEndpoint
{
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

    public static async Task<IResult> UpdateQuote(
        [FromRoute] Guid id,
        [FromBody] UpdateQuoteRequest request,
        [FromServices] IUpdateQuoteHandler handler,
        [FromServices] IValidator<UpdateQuoteHandlerRequest> validator,
        [FromServices] IMapper mapper,
        [FromServices] ILogger<UpdateQuoteEndpoint> logger,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["endpoint"] = nameof(UpdateQuoteEndpoint),
            ["public-id"] = id
        });

        logger.LogInformation("[{source}] mapping endpoint request to handler request", nameof(UpdateQuoteEndpoint));

        var handlerRequest = mapper.Map<UpdateQuoteRequest, UpdateQuoteHandlerRequest>(request, opt => opt.Items["Id"] = id);

        logger.LogInformation("[{source}] validating handler request", nameof(UpdateQuoteEndpoint));

        var isValidRequest = await validator.ValidateAsync(handlerRequest, cancellationToken);

        if (!isValidRequest.IsValid)
        {
            logger.LogError("[{source}] invalid handler request", nameof(UpdateQuoteEndpoint));

            return Results.ValidationProblem(isValidRequest.ToDictionary());
        }

        logger.LogInformation("[{source}] executing handler", nameof(UpdateQuoteEndpoint));

        var result = await handler.HandleAsync(handlerRequest, cancellationToken);

        if (result.IsFailure)
        {
            logger.LogError("[{source}] something went wrong, {error}", nameof(CreateQuoteEndpoint), result.Error);

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
