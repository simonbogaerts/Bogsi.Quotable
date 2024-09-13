using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes.GetQuoteById;
using Bogsi.Quotable.Application.Handlers.Quotes.GetQuoteByIdHandler;
using Microsoft.AspNetCore.Mvc;

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

public sealed class GetQuoteByIdEndpoint : IApiEndpoint
{
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("quotes/{id:guid}", GetQuoteById)
            .WithTags("Quotes")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    internal static async Task<IResult> GetQuoteById(
        [FromRoute] Guid id,
        [FromServices] IGetQuoteByIdHandler getQuoteByIdHandler,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        GetQuoteByIdHandlerRequest request = new()
        {
            PublicId = id
        };

        var quote = await getQuoteByIdHandler.HandleAsync(request, cancellationToken);

        if (quote.Quote is null)
        {
            return Results.NotFound();
        }

        var result = mapper.Map<GetQuoteByIdHandlerResponse, GetQuoteByIdResponse>(quote);

        return Results.Ok(result);
    }
}
