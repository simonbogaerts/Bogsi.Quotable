using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes.GetQuotes;
using Bogsi.Quotable.Application.Handlers.Quotes.GetQuotes;
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
        [FromServices] IGetQuotesHandler getQuotesHandler,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var request = mapper.Map<GetQuotesParameters, GetQuotesHandlerRequest>(parameters);

        var handlerResult = await getQuotesHandler.HandleAsync(request, cancellationToken);

        var result = mapper.Map<GetQuotesHandlerResponse, GetQuotesResponse>(handlerResult); 

        return Results.Ok(result);
    }
}
