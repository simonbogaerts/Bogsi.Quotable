using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Handlers.Quotes;
using Microsoft.AspNetCore.Mvc;

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

public sealed class GetQuoteByIdEndpoint : IApiEndpoint
{
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
            ["public-id"] = id
        });

        logger.LogInformation("[{source}] mapping endpoint request to handler request", nameof(GetQuoteByIdEndpoint));

        GetQuoteByIdHandlerRequest handlerRequest = new()
        {
            PublicId = id
        };

        logger.LogInformation("[{source}] executing handler", nameof(GetQuoteByIdEndpoint));

        var result = await handler.HandleAsync(handlerRequest, cancellationToken);

        if (result.IsFailure)
        {
            logger.LogError("[{source}] something went wrong, {error}", nameof(GetQuoteByIdEndpoint), result.Error);

            if (result.Error == QuotableErrors.NotFound)
            {
                return Results.NotFound();
            }
        }

        logger.LogInformation("[{source}] mapping handler response to endpoint response", nameof(GetQuoteByIdEndpoint));

        var response = mapper.Map<GetQuoteByIdHandlerResponse, GetQuoteByIdResponse>(result.Value);

        return Results.Ok(response);
    }
}
