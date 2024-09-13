namespace Bogsi.Quotable.Application.Contracts.Quotes.GetQuotes;

public sealed record GetQuotesResponse
{
    public required IEnumerable<QuoteResponseContract> Quotes { get; init; }
}