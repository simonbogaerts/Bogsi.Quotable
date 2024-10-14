// -----------------------------------------------------------------------
// <copyright file="CreateQuoteRequestBuilder.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Test.Builders.Requests;

using System.Diagnostics.CodeAnalysis;

using Bogsi.Quotable.Application.Contracts.Quotes;

/// <summary>
/// Builder for CreateQuoteRequest model.
/// </summary>
public sealed class CreateQuoteRequestBuilder : BuilderBase<CreateQuoteRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateQuoteRequestBuilder"/> class.
    /// </summary>
    [SetsRequiredMembers]
    public CreateQuoteRequestBuilder()
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
    public CreateQuoteRequestBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
