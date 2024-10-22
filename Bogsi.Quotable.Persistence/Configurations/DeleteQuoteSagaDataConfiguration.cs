// -----------------------------------------------------------------------
// <copyright file="DeleteQuoteSagaDataConfiguration.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Persistence.Configurations;

using Bogsi.Quotable.Application.Sagas;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Entity configuration for the DeleteQuoteSagaData.
/// </summary>
internal sealed record DeleteQuoteSagaDataConfiguration : IEntityTypeConfiguration<DeleteQuoteSagaData>, ISagaConfiguration
{
    /// <summary>
    /// Configuration of the entity.
    /// </summary>
    /// <param name="builder">Context builder.</param>
    public void Configure(EntityTypeBuilder<DeleteQuoteSagaData> builder)
    {
        builder.ToTable(
            Common.Constants.Database.Tables.DeleteQuoteSagaData,
            Common.Constants.Database.Schemas.Saga);

        builder
            .HasKey(x => x.CorrelationId);

        builder.Property(x => x.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .IsRowVersion();
    }
}
