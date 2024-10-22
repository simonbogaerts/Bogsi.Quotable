// -----------------------------------------------------------------------
// <copyright file="QuotableContext.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Persistence;

using Bogsi.Quotable.Application.Entities;

using MassTransit.Internals;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// The database context for quotable.
/// </summary>
/// <param name="options">Configuration options.</param>
public sealed class QuotableContext(DbContextOptions<QuotableContext> options)
    : DbContext(options)
{
    #region DbSets

    /// <summary>
    /// Gets the DbSet for the quote entities.
    /// </summary>
    public DbSet<QuoteEntity> Quotes => Set<QuoteEntity>();

    #endregion

    #region Protected Methods

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Constants.Schemas.Quotable);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IPersistenceMarker).Assembly,
            c => c.HasInterface<IQuotableConfiguration>());
    }

    #endregion
}
