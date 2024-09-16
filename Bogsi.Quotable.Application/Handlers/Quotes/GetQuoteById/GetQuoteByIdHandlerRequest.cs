namespace Bogsi.Quotable.Application.Handlers.Quotes.GetQuoteById;

public sealed record GetQuoteByIdHandlerRequest
{
    public Guid PublicId { get; init; }
}
