using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Persistence;
using Microsoft.EntityFrameworkCore;
using Bogsi.Quotable.Application.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bogsi.Quotable.Infrastructure.Utilities;

public sealed class UnitOfWork(QuotableContext quotable) : IUnitOfWork
{
    private readonly QuotableContext _quotable = quotable;

    public IDbTransaction BeginTransaction()
    {
        var transaction = _quotable.Database.BeginTransaction();

        return transaction.GetDbTransaction();
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        UpdateAuditableEntities();

        return await _quotable.SaveChangesAsync(cancellationToken: cancellationToken) >= 0;
    }

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
