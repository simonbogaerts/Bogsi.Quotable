using Bogsi.Quotable.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bogsi.Quotable.Persistence.Configurations;

internal sealed record QuoteEntityConfiguration : IEntityTypeConfiguration<QuoteEntity>
{
    #region Configure Method

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
            .HasMaxLength(ValueMaxLength);

        builder
            .Property(x => x.Created)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NOW()")
            .IsRequired();

        builder
            .Property(x => x.Updated)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("NOW()")
            .IsRequired()
            .IsConcurrencyToken();

        builder.HasData(Seed);
    }

    #endregion

    #region Configuration Values

    private const int ValueMaxLength = 1255;

    private static IEnumerable<QuoteEntity> Seed =>
    [
        new()
        {
            Id = 1,
            PublicId = Guid.NewGuid(),
            Value = "Ph'nglui mglw'nafh Cthulhu R'lyeh wgah'nagl fhtagn."
        },
        new()
        {
            Id = 2,
            PublicId = Guid.NewGuid(),
            Value = "That is not dead which can eternal lie, And with strange aeons even death may die."
        }
    ];

    #endregion
}
