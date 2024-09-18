using System.ComponentModel;
using Bogsi.Quotable.Application.Contracts.Abstract;

namespace Bogsi.Quotable.Application.Contracts.Quotes;


// struct is more performant than record
// defaults don't work as AsParameters supersedes these. Currently using mandatory defaults as fix.
public struct GetQuotesParameters
{
    public GetQuotesParameters()
    {
        PageNumber = Constants.PageNumber.Default;
        PageSize = Constants.PageSize.Default;
    }

    #region Pagination

    [DefaultValue(Constants.PageNumber.Default)]
    public int? PageNumber { get; init; }

    [DefaultValue(Constants.PageSize.Default)]
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

public sealed record GetQuotesSingleQuoteResponse : AbstractQuoteResponse
{

}

public sealed class GetQuotesResponse : List<GetQuotesSingleQuoteResponse>
{

}