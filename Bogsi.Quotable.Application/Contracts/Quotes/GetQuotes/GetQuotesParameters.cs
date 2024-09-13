using System.ComponentModel;

namespace Bogsi.Quotable.Application.Contracts.Quotes.GetQuotes;

// struct is more performant than record
// defaults don't work as AsParameters supersedes these. Currently using mandatory defaults as fix.
public struct GetQuotesParameters
{
    public GetQuotesParameters()
    {
        PageNumber = DefaultPageNumber;
        PageSize = DefaultPageSize;
    }

    #region Pagination

    public const int MinimumValue = 1;
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 10;
    public const int MaximumPageSize = 20;

    [DefaultValue(DefaultPageNumber)]
    public int? PageNumber { get; init; }

    [DefaultValue(DefaultPageSize)]
    public int? PageSize { get; init; }

    #endregion

    #region Additional

    // Filters 
    public string? Origin { get; init; }
    public string? Tag { get; init; }

    // Searching
    public string? SearchQuery { get; init; }

    // Ordering
    public string? OrderBy { get; init; }

    // Datashaping
    public string? Fields { get; init; }

    #endregion
}
