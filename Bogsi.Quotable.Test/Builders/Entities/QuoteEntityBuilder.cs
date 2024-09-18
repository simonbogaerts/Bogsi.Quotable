using System.Diagnostics.CodeAnalysis;
using Bogsi.Quotable.Application.Entities;

namespace Bogsi.Quotable.Test.Builders.Entities;

public sealed class QuoteEntityBuilder : BuilderBase<QuoteEntity>
{
    [SetsRequiredMembers]
    public QuoteEntityBuilder()
    {
        var now = DateTime.UtcNow;

        Instance = new()
        {
            Id = new Random().Next(),
            PublicId = Guid.NewGuid(),
            Created = now,
            Updated = now,
            Value = "Default-value"
        };
    }

    public QuoteEntityBuilder WithId(int id)
    {
        Instance = Instance with { Id = id };

        return this;
    }

    public QuoteEntityBuilder WithPublicId(Guid publicId)
    {
        Instance = Instance with { PublicId = publicId };

        return this;
    }

    public QuoteEntityBuilder WithAuditableDates(DateTime? created, DateTime? updated)
    {
        var now = DateTime.UtcNow;

        created = created ?? now;
        updated = updated ?? now;

        Instance = Instance with
        {
            Created = created.Value,
            Updated = updated.Value
        };

        return this;
    }

    public QuoteEntityBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
