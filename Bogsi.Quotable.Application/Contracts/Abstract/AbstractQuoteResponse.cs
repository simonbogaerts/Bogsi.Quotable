namespace Bogsi.Quotable.Application.Contracts.Abstract;

public abstract record AbstractQuoteResponse
{
    public required Guid PublicId { get; init; }
    public required string Value { get; init; }
}
