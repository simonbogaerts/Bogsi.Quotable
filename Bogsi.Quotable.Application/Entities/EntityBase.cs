// -----------------------------------------------------------------------
// <copyright file="EntityBase.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Entities;

/// <summary>
/// Interface that adds Created and Updated to an entity.
/// This interface is used in combination with unit of work.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets Created of the IAuditableEntity model.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets Updated of the IAuditableEntity model.
    /// </summary>
    public DateTime Updated { get; set; }
}

/// <summary>
/// Base entity that is used for inhertitance.
/// </summary>
public abstract record EntityBase : IAuditableEntity
{
    /// <summary>
    /// Gets Id of the EntityBase model.
    /// Used as an iternal id in the database.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets Id of the EntityBase model.
    /// Used as a public id.
    /// </summary>
    required public Guid PublicId { get; init; }

    /// <inheritdoc/>
    public DateTime Created { get; set; }

    /// <inheritdoc/>
    public DateTime Updated { get; set; }
}
