using Bogsi.Quotable.Application.Contracts.Abstract;

namespace Bogsi.Quotable.Application.Contracts.Quotes;

public sealed record GetQuoteByIdResponse : AbstractQuoteResponse
{
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}
