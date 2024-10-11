// -----------------------------------------------------------------------
// <copyright file="CreateQuoteEndpointContracts.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Contracts.Quotes;

using Bogsi.Quotable.Application.Contracts.Abstract;

/// <summary>
/// Representation of the request used in the Endpoint.
/// </summary>
public sealed record CreateQuoteRequest
{
    /// <summary>
    /// Gets Value of the CreateQuoteRequest model.
    /// </summary>
    required public string Value { get; init; }
}

/// <summary>
/// Representation of the response used in the Endpoint.
/// </summary>
public sealed record CreateQuoteResponse : AbstractQuoteResponse
{
    /// <summary>
    /// Gets Created of the CreateQuoteResponse model.
    /// </summary>
    public DateTime Created { get; init; }

    /// <summary>
    /// Gets Updated of the CreateQuoteResponse model.
    /// </summary>
    public DateTime Updated { get; init; }
}
