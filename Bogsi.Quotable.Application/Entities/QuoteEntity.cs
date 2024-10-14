// -----------------------------------------------------------------------
// <copyright file="QuoteEntity.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Entities;

/// <summary>
/// The entity used for database representation of the quote model.
/// </summary>
public sealed record QuoteEntity : EntityBase
{
    /// <summary>
    /// Gets Value of the QuoteEntity model.
    /// </summary>
    required public string Value { get; init; }
}
