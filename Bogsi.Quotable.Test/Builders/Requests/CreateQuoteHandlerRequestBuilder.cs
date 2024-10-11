// -----------------------------------------------------------------------
// <copyright file="CreateQuoteHandlerRequestBuilder.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Test.Builders.Requests;

using System.Diagnostics.CodeAnalysis;

using Bogsi.Quotable.Application.Handlers.Quotes;

/// <summary>
/// Builder for CreateQuoteHandlerRequest model.
/// </summary>
public sealed class CreateQuoteHandlerRequestBuilder : BuilderBase<CreateQuoteHandlerRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateQuoteHandlerRequestBuilder"/> class.
    /// </summary>
    [SetsRequiredMembers]
    public CreateQuoteHandlerRequestBuilder()
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
    public CreateQuoteHandlerRequestBuilder WithValue(string value)
    {
        Instance = Instance with { Value = value };

        return this;
    }
}
