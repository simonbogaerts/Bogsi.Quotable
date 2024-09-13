namespace Bogsi.Quotable.Application.Handlers.Quotes.GetQuoteByIdHandler;

public sealed record GetQuoteByIdHandlerResponse
{
    public required QuoteResponseHandler? Quote { get; init; }
}
