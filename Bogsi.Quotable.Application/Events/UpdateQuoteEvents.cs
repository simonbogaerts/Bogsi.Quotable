// -----------------------------------------------------------------------
// <copyright file="UpdateQuoteEvents.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Events;

using Bogsi.Quotable.Application.Models;

/// <summary>
///  Message that requests an update of an existing quote.
/// </summary>
public sealed record UpdateQuoteRequestedEvent : BaseEvent
{
    /// <summary>
    /// Gets the quote model that needs to be updated.
    /// </summary>
    required public Quote Model { get; init; }
}

/// <summary>
///  Message that triggers the update of an existing quote.
/// </summary>
public sealed record UpdateQuoteEvent : BaseEvent
{
    /// <summary>
    /// Gets the quote model that needs to be updated.
    /// </summary>
    required public Quote Model { get; init; }
}

/// <summary>
/// Event when a quote is updated.
/// </summary>
public sealed record UpdateQuoteCompletedEvent : BaseEvent
{
    /// <summary>
    /// Gets the quote model that needs to be cached.
    /// </summary>
    public Quote? Model { get; init; }
}
