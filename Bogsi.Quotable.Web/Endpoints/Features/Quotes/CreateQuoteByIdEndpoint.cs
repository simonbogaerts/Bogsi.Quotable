using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;

using Microsoft.AspNetCore.Mvc;

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

public sealed class CreateQuoteByIdEndpoint : IApiEndpoint
{
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("quotes/{id:guid}", BlockCreateQuote)
            .MapToApiVersion(1)
            .ExcludeFromDescription();
    }

    internal static async Task<IResult> BlockCreateQuote(
        [FromRoute] Guid id,
        [FromServices] IReadonlyRepository<Quote> repository,
        [FromServices] ILogger<CreateQuoteByIdEndpoint> logger,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            ["endpoint"] = nameof(CreateQuoteByIdEndpoint),
            ["public-id"] = id
        });

        logger.LogInformation("[{source}] checking if item exists", nameof(CreateQuoteByIdEndpoint));

        var result = await repository.ExistsAsync(id, cancellationToken);

        return !result.Value
            ? Results.NotFound()
            : Results.Conflict();
    }
}
