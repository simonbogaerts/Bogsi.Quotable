// -----------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Infrastructure.Utilities;

using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Persistence;
using Microsoft.EntityFrameworkCore;
using Bogsi.Quotable.Application.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

/// <summary>
/// Provides everything that needs to be done to change the database as a result of the work.
/// </summary>
/// <param name="quotable">The database context.</param>
public sealed class UnitOfWork(QuotableContext quotable) : IUnitOfWork
{
    private readonly QuotableContext _quotable = quotable;

    /// <inheritdoc/>
    public IDbTransaction BeginTransaction()
    {
        var transaction = _quotable.Database.BeginTransaction();

        return transaction.GetDbTransaction();
    }

    /// <inheritdoc/>
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        UpdateAuditableEntities();

        return await _quotable.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false) >= 0;
    }

    /// <summary>
    /// Updates the AuditableEntities with the correct Created and Updated values.
    /// </summary>
    private void UpdateAuditableEntities()
    {
        var now = DateTime.UtcNow;
        var entities = _quotable.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entity in entities)
        {
            if (entity.State is EntityState.Added)
            {
                entity.Property(x => x.Created).CurrentValue = now;
                entity.Property(x => x.Updated).CurrentValue = now;
            }

            if (entity.State is EntityState.Modified)
            {
                entity.Property(x => x.Updated).CurrentValue = now;
            }
        }
    }
}
