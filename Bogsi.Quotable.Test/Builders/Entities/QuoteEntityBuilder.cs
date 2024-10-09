// -----------------------------------------------------------------------
// <copyright file="QuoteEntityBuilder.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Test.Builders.Entities;

using System.Diagnostics.CodeAnalysis;

using Bogsi.Quotable.Application.Entities;

/// <summary>
/// Builder for QuoteEntity model.
/// </summary>
public sealed class QuoteEntityBuilder : BuilderBase<QuoteEntity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuoteEntityBuilder"/> class.
    /// </summary>
    [SetsRequiredMembers]
    public QuoteEntityBuilder()
    {
        var now = DateTime.UtcNow;

        Instance = new ()
        {
            Id = new Random().Next(),
            PublicId = Guid.NewGuid(),
            Created = now,
            Updated = now,
            Value = "DEFAULT-VALUE",
        };
    }

    /// <summary>
    /// Add a private id.
    /// </summary>
    /// <param name="id">private id.</param>
    /// <returns>builder with private id configured.</returns>
    public QuoteEntityBuilder WithId(int id)
    {
        Instance = Instance with { Id = id };

        return this;
    }

    /// <summary>
    /// Add a public id.
    /// </summary>
    /// <param name="publicId">public id.</param>
    /// <returns>builder with public id configured.</returns>
    public QuoteEntityBuilder WithPublicId(Guid publicId)
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

    /// <summary>
    /// Add value.
    /// </summary>
    /// <param name="value">Value of the model.</param>
    /// <returns>Builder with value configured.</returns>
    public QuoteEntityBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
