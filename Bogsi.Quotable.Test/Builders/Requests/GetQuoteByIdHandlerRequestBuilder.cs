// -----------------------------------------------------------------------
// <copyright file="GetQuoteByIdHandlerRequestBuilder.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Test.Builders.Requests;

using System.Diagnostics.CodeAnalysis;

using Bogsi.Quotable.Application.Handlers.Quotes;

/// <summary>
/// Builder for GetQuoteByIdHandlerRequest model.
/// </summary>
public sealed class GetQuoteByIdHandlerRequestBuilder : BuilderBase<GetQuoteByIdHandlerRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetQuoteByIdHandlerRequestBuilder"/> class.
    /// </summary>
    [SetsRequiredMembers]
    public GetQuoteByIdHandlerRequestBuilder()
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
    public GetQuoteByIdHandlerRequestBuilder WithPublicId(Guid publicId)
    {
        Instance = Instance with { PublicId = publicId };

        return this;
    }
}
