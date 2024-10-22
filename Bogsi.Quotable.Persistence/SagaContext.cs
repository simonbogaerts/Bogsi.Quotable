// -----------------------------------------------------------------------
// <copyright file="SagaContext.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Persistence;

using Bogsi.Quotable.Application.Sagas;

using MassTransit.Internals;

using Microsoft.EntityFrameworkCore;

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

    #region DbSets

    /// <summary>
    /// Gets the DbSet for the create quote saga data.
    /// </summary>
    public DbSet<CreateQuoteSagaData> CreateQuoteSagaData => Set<CreateQuoteSagaData>();

    /// <summary>
    /// Gets the DbSet for the create quote saga data.
    /// </summary>
    public DbSet<UpdateQuoteSagaData> UpdateQuoteSagaData => Set<UpdateQuoteSagaData>();

    /// <summary>
    /// Gets the DbSet for the create quote saga data.
    /// </summary>
    public DbSet<DeleteQuoteSagaData> DeleteQuoteSagaData => Set<DeleteQuoteSagaData>();

    #endregion

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Common.Constants.Database.Schemas.Saga);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IPersistenceMarker).Assembly,
            c => c.HasInterface<ISagaConfiguration>());
    }
}
