namespace Bogsi.Quotable.Application.Entities;

public sealed record QuoteEntity : EntityBase
{
    public required string Value { get; init; }
}
