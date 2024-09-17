namespace Bogsi.Quotable.Application.Contracts.Quotes.UpdateQuote;

public sealed record UpdateQuoteRequest
{
    public required string Value { get; init; }
}
