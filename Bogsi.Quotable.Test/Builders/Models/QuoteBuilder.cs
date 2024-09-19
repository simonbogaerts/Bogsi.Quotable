using System.Diagnostics.CodeAnalysis;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Test.Builders.Models;

public sealed class QuoteBuilder : BuilderBase<Quote>
{
    [SetsRequiredMembers]
    public QuoteBuilder()
    {
        var now = DateTime.UtcNow;

        Instance = new()
        {
            PublicId = Guid.NewGuid(),
            Created = now,
            Updated = now,
            Value = "DEFAULT-VALUE"
        };
    }

    public QuoteBuilder WithPublicId(Guid publicId)
    {
        Instance = Instance with { PublicId = publicId };

        return this;
    }

    public QuoteBuilder WithAuditableDates(DateTime? created, DateTime? updated)
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

    public QuoteBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
