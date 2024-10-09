// -----------------------------------------------------------------------
// <copyright file="CursoredList.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Utilities;

/// <summary>
/// A list that contains items starting from a specific cursor or higher.
/// Used during cursor pagination.
/// </summary>
/// <typeparam name="T">Type of item the list contains.</typeparam>
public record CursorResponse<T>
{
    /// <summary>
    /// Gets the id where the cursor pagination should start.
    /// </summary>
    public int Cursor { get; init; }

    /// <summary>
    /// Gets the requested size of the response.
    /// </summary>
    public int Size { get; init; }

    /// <summary>
    /// Gets the number of items matching the request parameters.
    /// </summary>
    public int Total { get; init; }

    /// <summary>
    /// Gets a value indicating whether there is more data left.
    /// </summary>
    public bool HasNext { get; init; }

    /// <summary>
    /// Gets the data requested of type T.
    /// </summary>
    required public IReadOnlyCollection<T> Data { get; init; }
}
