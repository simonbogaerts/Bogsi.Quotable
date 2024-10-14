// -----------------------------------------------------------------------
// <copyright file="Quote.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Models;

/// <summary>
/// The entity used for business logic representation of the quote model.
/// </summary>
public sealed record Quote : ModelBase
{
    /// <summary>
    /// Gets Value of the Quote model.
    /// </summary>
    required public string Value { get; init; }
}
