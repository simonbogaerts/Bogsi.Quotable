// -----------------------------------------------------------------------
// <copyright file="CreateQuoteSagaDataConfiguration.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Persistence.Configurations;

using Bogsi.Quotable.Application.Sagas;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Entity configuration for the CreateQuoteSagaData.
/// </summary>
internal sealed record CreateQuoteSagaDataConfiguration : IEntityTypeConfiguration<CreateQuoteSagaData>, ISagaConfiguration
{
    /// <summary>
    /// Configuration of the entity.
    /// </summary>
    /// <param name="builder">Context builder.</param>
    public void Configure(EntityTypeBuilder<CreateQuoteSagaData> builder)
    {
        builder.ToTable(Constants.Tables.CreateQuoteSagaData, Constants.Schemas.Saga);

        builder
            .HasKey(x => x.CorrelationId);

        builder.Property(x => x.RowVersion)
            .HasColumnName("xmin")
            .HasColumnType("xid")
            .IsRowVersion();
    }
}
