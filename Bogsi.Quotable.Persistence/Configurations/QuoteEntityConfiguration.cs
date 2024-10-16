﻿// -----------------------------------------------------------------------
// <copyright file="QuoteEntityConfiguration.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Persistence.Configurations;

using Bogsi.Quotable.Application.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Entity configuration for the QuoteEntity.
/// </summary>
internal sealed record QuoteEntityConfiguration : IEntityTypeConfiguration<QuoteEntity>
{
    #region Configure Method

    /// <summary>
    /// Configuration of the entity.
    /// </summary>
    /// <param name="builder">Context builder.</param>
    public void Configure(EntityTypeBuilder<QuoteEntity> builder)
    {
        builder.ToTable(Constants.Tables.Quotes, Constants.Schemas.Quotable);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.PublicId)
            .IsRequired()
            .ValueGeneratedNever();

        builder
            .Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(Application.Constants.Quote.Properties.Value.MaximumLength);

        builder
            .Property(x => x.Created)
            .HasDefaultValueSql(Constants.Functions.GetDate)
            .IsRequired();

        builder
            .Property(x => x.Updated)
            .HasDefaultValueSql(Constants.Functions.GetDate)
            .IsRequired()
            .IsConcurrencyToken();

        builder.HasData(Seed);
    }

    #endregion

    #region Configuration Values

    private static IEnumerable<QuoteEntity> Seed =>
    [
        new ()
        {
            Id = 1,
            PublicId = Guid.NewGuid(),
            Value = "Ph'nglui mglw'nafh Cthulhu R'lyeh wgah'nagl fhtagn.",
        },
        new ()
        {
            Id = 2,
            PublicId = Guid.NewGuid(),
            Value = "That is not dead which can eternal lie, And with strange aeons even death may die.",
        }

    ];

    #endregion
}
