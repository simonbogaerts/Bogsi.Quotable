// -----------------------------------------------------------------------
// <copyright file="SagaContext.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace Bogsi.Quotable.Persistence;

/// <summary>
/// The database context for sagas.
/// </summary>
public sealed class SagaContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SagaContext"/> class.
    /// </summary>
    /// <param name="options">Configuration options.</param>
    public SagaContext(DbContextOptions<SagaContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Constants.Schemas.Saga);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IPersistenceMarker).Assembly);
    }
}
