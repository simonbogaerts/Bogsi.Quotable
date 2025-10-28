// -----------------------------------------------------------------------
// <copyright file="UpdateQuoteSagaDataConfiguration.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Persistence.Configurations;

using Bogsi.Quotable.Application.Sagas;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Entity configuration for the UpdateQuoteSagaData.
/// </summary>
internal sealed record UpdateQuoteSagaDataConfiguration : IEntityTypeConfiguration<UpdateQuoteSagaData>, ISagaConfiguration
{
    /// <summary>
    /// Configuration of the entity.
    /// </summary>
    /// <param name="builder">Context builder.</param>
    public void Configure(EntityTypeBuilder<UpdateQuoteSagaData> builder)
    {
        builder.ToTable(
            Common.Constants.Database.Tables.UpdateQuoteSagaData,
            Common.Constants.Database.Schemas.Saga);

        builder
            .HasKey(x => x.CorrelationId);

        builder.Property(x => x.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .IsRowVersion();
    }
}
