// -----------------------------------------------------------------------
// <copyright file="DeleteQuoteHandlerRequestBuilder.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Test.Builders.Requests;

using System.Diagnostics.CodeAnalysis;

using Bogsi.Quotable.Application.Handlers.Quotes;

/// <summary>
/// Builder for DeleteQuoteHandlerRequest model.
/// </summary>
public sealed class DeleteQuoteHandlerRequestBuilder : BuilderBase<DeleteQuoteHandlerRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteQuoteHandlerRequestBuilder"/> class.
    /// </summary>
    [SetsRequiredMembers]
    public DeleteQuoteHandlerRequestBuilder()
    {
        Instance = new ()
        {
            PublicId = Guid.NewGuid(),
        };
    }

    /// <summary>
    /// Add a public id.
    /// </summary>
    /// <param name="publicId">public id.</param>
    /// <returns>builder with public id configured.</returns>
    public DeleteQuoteHandlerRequestBuilder WithPublicId(Guid publicId)
    {
        Instance = Instance with { PublicId = publicId };

        return this;
    }
}
