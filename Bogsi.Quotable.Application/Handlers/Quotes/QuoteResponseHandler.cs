namespace Bogsi.Quotable.Application.Handlers.Quotes;

public sealed record QuoteResponseHandler
{
    public required Guid PublicId { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
    public required string Value { get; init; }
}
