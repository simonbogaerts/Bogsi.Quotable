namespace Bogsi.Quotable.Application.Handlers.Quotes.GetQuoteById;

public sealed record GetQuoteByIdHandlerResponse
{
    public required QuoteResponseHandler? Quote { get; init; }
}
