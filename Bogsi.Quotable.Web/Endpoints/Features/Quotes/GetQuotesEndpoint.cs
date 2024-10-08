using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes;

using Microsoft.AspNetCore.Mvc;

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

public sealed class GetQuotesEndpoint : IApiEndpoint
{
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
            ["@parameters"] = parameters
        });

        logger.LogInformation("[{source}] mapping parameters to handler request", nameof(GetQuotesEndpoint));

        var handlerRequest = mapper.Map<GetQuotesParameters, GetQuotesHandlerRequest>(parameters);

        logger.LogInformation("[{source}] executing handler", nameof(GetQuotesEndpoint));

        var result = await handler.HandleAsync(handlerRequest, cancellationToken);

        if (result.IsFailure)
        {
            logger.LogError("[{source}] something went wrong, {error}", nameof(GetQuotesEndpoint), result.Error);

            return Results.Problem(statusCode: 500);
        }

        logger.LogInformation("[{source}] mapping handler response to endpoint response", nameof(GetQuotesEndpoint));

        var response = mapper.Map<GetQuotesHandlerResponse?, GetQuotesResponse>(result.Value);

        return Results.Ok(response);
    }
}
