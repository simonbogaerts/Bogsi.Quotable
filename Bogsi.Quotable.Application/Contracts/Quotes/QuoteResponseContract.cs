namespace Bogsi.Quotable.Application.Contracts.Quotes;

public sealed record QuoteResponseContract
{
    public required Guid PublicId { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
    public required string Value { get; init; }
}