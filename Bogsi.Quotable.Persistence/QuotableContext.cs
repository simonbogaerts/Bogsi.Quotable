using Bogsi.Quotable.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bogsi.Quotable.Persistence;

public sealed class QuotableContext(DbContextOptions<QuotableContext> options)
    : DbContext(options)
{
    #region DbSets

    public DbSet<QuoteEntity> Quotes => Set<QuoteEntity>();

    #endregion

    #region Protected Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.Schemas.Quotable);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IPersistenceMarker).Assembly);
    }

    #endregion
}