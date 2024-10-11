// -----------------------------------------------------------------------
// <copyright file="UpdateQuoteEndpointContracts.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Contracts.Quotes;

/// <summary>
/// Representation of the request used in the Endpoint.
/// </summary>
public sealed record UpdateQuoteRequest
{
    /// <summary>
    /// Gets Value of the UpdateQuoteRequest model.
    /// </summary>
    required public string Value { get; init; }
}
