// <copyright file="AbstractQuoteResponse.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>

namespace Bogsi.Quotable.Application.Contracts.Abstract;

/// <summary>
/// Abstract base class for internal use of the Quote model.
/// </summary>
public abstract record AbstractQuoteResponse
{
    /// <summary>
    /// Gets public Id of the Quote model.
    /// </summary>
    required public Guid PublicId { get; init; }

    /// <summary>
    /// Gets Value of the Quote model.
    /// </summary>
    required public string Value { get; init; }
}
