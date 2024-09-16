namespace Bogsi.Quotable.Application.Handlers.Quotes.CreateQuote;

public sealed record CreateQuoteHandlerRequest
{
    public const int MinimumLength = 5;
    public const int MaximumLength = 1255;

    public required string Value { get; init; }
}
