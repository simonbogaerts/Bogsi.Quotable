namespace Bogsi.Quotable.Application.Handlers.Quotes.GetQuoteByIdHandler;

public sealed record GetQuoteByIdHandlerRequest
{
    public Guid PublicId { get; init; }
}
