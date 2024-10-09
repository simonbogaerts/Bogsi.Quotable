// <copyright file="ModelBase.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>

namespace Bogsi.Quotable.Application.Models;

/// <summary>
/// Abstract base class for all models.
/// </summary>
public abstract record ModelBase
{
    /// <summary>
    /// Gets public Id of the Quote model.
    /// </summary>
    required public Guid PublicId { get; init; }

    /// <summary>
    /// Gets Created property.
    /// </summary>
    public DateTime Created { get; init; }

    /// <summary>
    /// Gets Updated property.
    /// </summary>
    public DateTime Updated { get; init; }
}
