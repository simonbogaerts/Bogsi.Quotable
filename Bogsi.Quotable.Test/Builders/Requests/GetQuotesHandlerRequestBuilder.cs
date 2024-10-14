// -----------------------------------------------------------------------
// <copyright file="GetQuotesHandlerRequestBuilder.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Test.Builders.Requests;

using System.Diagnostics.CodeAnalysis;

using Bogsi.Quotable.Application.Handlers.Quotes;

/// <summary>
/// Builder for GetQuotesHandlerRequest model.
/// </summary>
public sealed class GetQuotesHandlerRequestBuilder : BuilderBase<GetQuotesHandlerRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetQuotesHandlerRequestBuilder"/> class.
    /// </summary>
    [SetsRequiredMembers]
    public GetQuotesHandlerRequestBuilder()
    {
        Instance = new GetQuotesHandlerRequest()
        {
            Cursor = Application.Constants.Cursor.Default,
            Size = Application.Constants.Size.Default,
        };
    }

    /// <summary>
    /// Add cursor.
    /// </summary>
    /// <param name="cursor">Starting cursor value.</param>
    /// <returns>builder with cursor configured.</returns>
    public GetQuotesHandlerRequestBuilder WithCursor(int cursor)
    {
        Instance = Instance with { Cursor = cursor };

        return this;
    }

    /// <summary>
    /// Add size.
    /// </summary>
    /// <param name="size">Requested size of the response.</param>
    /// <returns>builder with size configured.</returns>
    public GetQuotesHandlerRequestBuilder WithSize(int size)
    {
        Instance = Instance with { Size = size };

        return this;
    }

    /// <summary>
    /// Add origin.
    /// </summary>
    /// <param name="origin">Origin.</param>
    /// <returns>builder with origin configured.</returns>
    public GetQuotesHandlerRequestBuilder WithOrigin(string origin)
    {
        Instance = Instance with { Origin = origin };

        return this;
    }

    /// <summary>
    /// Add tag.
    /// </summary>
    /// <param name="tag">Tag of the quote.</param>
    /// <returns>builder with tag configured.</returns>
    public GetQuotesHandlerRequestBuilder WithTags(string tag)
    {
        Instance = Instance with { Tag = tag };

        return this;
    }

    /// <summary>
    /// Add search query.
    /// </summary>
    /// <param name="searchQuery">Search query to filter on.</param>
    /// <returns>builder with searchquery configured.</returns>
    public GetQuotesHandlerRequestBuilder WithSearchQuery(string searchQuery)
    {
        Instance = Instance with { SearchQuery = searchQuery };

        return this;
    }
}
