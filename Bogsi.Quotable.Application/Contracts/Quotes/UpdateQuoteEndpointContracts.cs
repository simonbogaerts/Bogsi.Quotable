namespace Bogsi.Quotable.Application.Contracts.Quotes;

public sealed record UpdateQuoteRequest
{
    public required string Value { get; init; }
}