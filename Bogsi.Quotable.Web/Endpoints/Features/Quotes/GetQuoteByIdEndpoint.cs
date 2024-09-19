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
        CancellationToken cancellationToken)
    {
        GetQuoteByIdHandlerRequest handlerRequest = new()
        {
            PublicId = id
        };

        var result = await handler.HandleAsync(handlerRequest, cancellationToken);

        if (result.IsFailure)
        {
            if (result.Error == QuotableErrors.NotFound)
            {
                return Results.NotFound();
            }
        }

        var response = mapper.Map<GetQuoteByIdHandlerResponse, GetQuoteByIdResponse>(result.Value);

        return Results.Ok(response);
    }
}
