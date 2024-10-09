// -----------------------------------------------------------------------
// <copyright file="UpdateQuoteRequestBuilder.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Test.Builders.Requests;

using System.Diagnostics.CodeAnalysis;

using Bogsi.Quotable.Application.Contracts.Quotes;

/// <summary>
/// Builder for UpdateQuoteRequest model.
/// </summary>
public class UpdateQuoteRequestBuilder : BuilderBase<UpdateQuoteRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateQuoteRequestBuilder"/> class.
    /// </summary>
    [SetsRequiredMembers]
    public UpdateQuoteRequestBuilder()
    {
        Instance = new ()
        {
            Value = "DEFAULT-VALUE",
        };
    }

    /// <summary>
    /// Add value.
    /// </summary>
    /// <param name="value">Value of the model.</param>
    /// <returns>Builder with value configured.</returns>
    public UpdateQuoteRequestBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
