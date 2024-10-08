using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Handlers.Quotes;

using Microsoft.AspNetCore.Mvc;

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

public sealed class DeleteQuoteEndpoint : IApiEndpoint
{
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapDelete("quotes/{id:guid}", DeleteQuote)
            .WithTags(Constants.Endpoints.Quotes)
            .WithName(Constants.Endpoints.QuoteEndpoints.DeleteQuoteEndpoint)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    internal static async Task<IResult> DeleteQuote(
        [FromRoute] Guid id,
        [FromServices] IDeleteQuoteHandler handler,
        [FromServices] ILogger<DeleteQuoteEndpoint> logger,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["endpoint"] = nameof(DeleteQuoteEndpoint),
            ["public-id"] = id
        });

        logger.LogInformation("[{source}] mapping endpoint request to handler request", nameof(DeleteQuoteEndpoint));

        DeleteQuoteHandlerRequest request = new() 
        { 
            PublicId = id 
        };

        logger.LogInformation("[{source}] executing handler", nameof(DeleteQuoteEndpoint));

        var result = await handler.HandleAsync(request, cancellationToken);

        if (result.IsFailure)
        {
            logger.LogError("[{source}] something went wrong, {error}", nameof(DeleteQuoteEndpoint), result.Error);

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
