// -----------------------------------------------------------------------
// <copyright file="UpdateQuoteHandlerRequestBuilder.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Test.Builders.Requests;

using System.Diagnostics.CodeAnalysis;

using Bogsi.Quotable.Application.Handlers.Quotes;

/// <summary>
/// Builder for UpdateQuoteHandlerRequest model.
/// </summary>
public sealed class UpdateQuoteHandlerRequestBuilder : BuilderBase<UpdateQuoteHandlerRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateQuoteHandlerRequestBuilder"/> class.
    /// </summary>
    [SetsRequiredMembers]
    public UpdateQuoteHandlerRequestBuilder()
    {
        Instance = new ()
        {
            PublicId = Guid.NewGuid(),
            Value = "DEFAULT-VALUE",
        };
    }

    /// <summary>
    /// Add a public id.
    /// </summary>
    /// <param name="publicId">public id.</param>
    /// <returns>builder with public id configured.</returns>
    public UpdateQuoteHandlerRequestBuilder WithPublicId(Guid publicId)
    {
        Instance = Instance with { PublicId = publicId };

        return this;
    }

    /// <summary>
    /// Add value.
    /// </summary>
    /// <param name="value">Value of the model.</param>
    /// <returns>Builder with value configured.</returns>
    public UpdateQuoteHandlerRequestBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
