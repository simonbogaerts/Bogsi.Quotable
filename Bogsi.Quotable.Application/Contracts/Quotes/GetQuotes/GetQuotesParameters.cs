using System.ComponentModel;

namespace Bogsi.Quotable.Application.Contracts.Quotes.GetQuotes;

// struct is more performant than record
// defaults don't work as AsParameters supersedes these. Currently using mandatory defaults as fix.
public struct GetQuotesParameters
{
    public GetQuotesParameters()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    #region Pagination

    private const int MaxPageSize = 20;

    [DefaultValue(1)]
    public int PageNumber { get; init; }

    private int _pageSize = 10;

    [DefaultValue(10)]
    public int PageSize
    {
        readonly get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

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
