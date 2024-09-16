namespace Bogsi.Quotable.Application.Handlers.Quotes.CreateQuote;

public sealed record CreateQuoteHandlerResponse
{
    public required Guid PublicId { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
    public required string Value { get; init; }
}
