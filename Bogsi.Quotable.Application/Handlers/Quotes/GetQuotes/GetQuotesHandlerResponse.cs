namespace Bogsi.Quotable.Application.Handlers.Quotes.GetQuotes;

public sealed record GetQuotesHandlerResponse
{
    public required IEnumerable<QuoteResponseHandler> Quotes { get; init; }
}
