// -----------------------------------------------------------------------
// <copyright file="QuotableErrors.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Errors;

/// <summary>
/// Represents the error object used internally.
/// </summary>
/// <param name="Code">The identifier of the error.</param>
/// <param name="Description">The detailed description of the error.</param>
public sealed record QuotableError(string Code, string Description);

/// <summary>
/// A collection containing flyweights of the most common errors.
/// </summary>
public sealed record QuotableErrors
{
    /// <summary>
    /// Gets error for when an item is not found.
    /// </summary>
    public static QuotableError NotFound => new (
        nameof(NotFound),
        "Nothing found for provided id");

    /// <summary>
    /// Gets error for when request or input is null.
    /// </summary>
    public static QuotableError InputRequired => new (
        nameof(InputRequired),
        "Required input or request is null");

    /// <summary>
    /// Gets error for internal issue.
    /// </summary>
    public static QuotableError InternalError => new (
        nameof(InternalError),
        "Something went wrong");
}
