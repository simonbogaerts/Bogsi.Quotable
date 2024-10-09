// -----------------------------------------------------------------------
// <copyright file="GetQuotesEndpointContracts.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Contracts.Quotes;

using System.ComponentModel;

using Bogsi.Quotable.Application.Contracts.Abstract;
using Bogsi.Quotable.Application.Utilities;

// struct is more performant than record
// defaults don't work as AsParameters supersedes these. Currently using mandatory defaults as fix.

/// <summary>
/// Representation of the parameters that can be used in the Endpoint.
/// </summary>
public readonly struct GetQuotesParameters
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetQuotesParameters"/> struct.
    /// </summary>
    public GetQuotesParameters()
    {
        Cursor = Constants.Cursor.Default;
        Size = Constants.Size.Default;
    }

    #region Pagination

    /// <summary>
    /// Gets the Cursor of the GetQuotesParameters model.
    /// </summary>
    [DefaultValue(Constants.Cursor.Default)]
    public int? Cursor { get; init; }

    /// <summary>
    /// Gets the Size of the GetQuotesParameters model.
    /// </summary>
    [DefaultValue(Constants.Size.Default)]
    public int? Size { get; init; }

    #endregion

    #region Additional

    /// <summary>
    /// Gets the Origin of the GetQuotesParameters model.
    /// </summary>
    public string? Origin { get; init; }

    /// <summary>
    /// Gets the Tag of the GetQuotesParameters model.
    /// </summary>
    public string? Tag { get; init; }

    /// <summary>
    /// Gets the SearchQuery of the GetQuotesParameters model.
    /// </summary>
    public string? SearchQuery { get; init; }

    #endregion
}

/// <summary>
/// Representation of the response used in the Endpoint (single item).
/// </summary>
public sealed record GetQuotesSingleQuoteResponse : AbstractQuoteResponse
{

}

/// <summary>
/// Representation of the response used in the Endpoint (collection).
/// </summary>
public sealed record GetQuotesResponse : CursorResponse<GetQuotesSingleQuoteResponse>
{

}
