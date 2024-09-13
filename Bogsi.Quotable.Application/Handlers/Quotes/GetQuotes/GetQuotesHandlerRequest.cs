namespace Bogsi.Quotable.Application.Handlers.Quotes.GetQuotes;

public sealed record GetQuotesHandlerRequest
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public string? Origin { get; init; }
    public string? Tag { get; init; }
    public string? SearchQuery { get; init; }
    public string? OrderBy { get; init; }
    public string? Fields { get; init; }
}
