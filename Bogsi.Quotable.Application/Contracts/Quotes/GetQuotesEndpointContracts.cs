using System.ComponentModel;
using Bogsi.Quotable.Application.Contracts.Abstract;
using Bogsi.Quotable.Application.Utilities;

namespace Bogsi.Quotable.Application.Contracts.Quotes;


// struct is more performant than record
// defaults don't work as AsParameters supersedes these. Currently using mandatory defaults as fix.
public struct GetQuotesParameters
{
    public GetQuotesParameters()
    {
        Cursor = Constants.Cursor.Default;
        Size = Constants.Size.Default;
    }

    #region Pagination

    [DefaultValue(Constants.Cursor.Default)]
    public int? Cursor { get; init; }

    [DefaultValue(Constants.Size.Default)]
    public int? Size { get; init; }

    #endregion

    #region Additional

    public string? Origin { get; init; }
    public string? Tag { get; init; }
    public string? SearchQuery { get; init; }

    #endregion
}

public sealed record GetQuotesSingleQuoteResponse : AbstractQuoteResponse
{

}

public sealed record GetQuotesResponse : CursorResponse<List<GetQuotesSingleQuoteResponse>>
{

}