namespace Bogsi.Quotable.Application.Contracts.Quotes.GetQuoteById;

public sealed record GetQuoteByIdResponse
{
    public required QuoteResponseContract Quote { get; init; }
}
