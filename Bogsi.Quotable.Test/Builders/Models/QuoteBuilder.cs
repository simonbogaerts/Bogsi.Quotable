// -----------------------------------------------------------------------
// <copyright file="QuoteBuilder.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Test.Builders.Models;

using System.Diagnostics.CodeAnalysis;

using Bogsi.Quotable.Application.Models;

/// <summary>
/// Builder for Quote model.
/// </summary>
public sealed class QuoteBuilder : BuilderBase<Quote>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuoteBuilder"/> class.
    /// </summary>
    [SetsRequiredMembers]
    public QuoteBuilder()
    {
        var now = DateTime.UtcNow;

        Instance = new ()
        {
            PublicId = Guid.NewGuid(),
            Created = now,
            Updated = now,
            Value = "DEFAULT-VALUE",
        };
    }

    /// <summary>
    /// Add a public id.
    /// </summary>
    /// <param name="publicId">public id.</param>
    /// <returns>builder with public id configured.</returns>
    public QuoteBuilder WithPublicId(Guid publicId)
    {
        Instance = Instance with { PublicId = publicId };

        return this;
    }

    /// <summary>
    /// Add Created and/or Updated.
    /// </summary>
    /// <param name="created">Created datetime.</param>
    /// <param name="updated">Updated datetime.</param>
    /// <returns>Builder with provided dates configured.</returns>
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

    /// <summary>
    /// Add value.
    /// </summary>
    /// <param name="value">Value of the model.</param>
    /// <returns>Builder with value configured.</returns>
    public QuoteBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
