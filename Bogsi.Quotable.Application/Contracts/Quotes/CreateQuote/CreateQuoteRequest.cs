namespace Bogsi.Quotable.Application.Contracts.Quotes.CreateQuote;

public sealed record CreateQuoteRequest
{
    public required string Value { get; init; }
}
