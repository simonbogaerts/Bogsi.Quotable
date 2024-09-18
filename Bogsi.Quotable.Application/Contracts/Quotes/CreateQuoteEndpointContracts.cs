using Bogsi.Quotable.Application.Contracts.Abstract;

namespace Bogsi.Quotable.Application.Contracts.Quotes;

public sealed record CreateQuoteRequest
{
    public required string Value { get; init; }
}

public sealed record CreateQuoteResponse : AbstractQuoteResponse
{
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}