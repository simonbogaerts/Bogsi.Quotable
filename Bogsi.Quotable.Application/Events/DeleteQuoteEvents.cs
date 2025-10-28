// -----------------------------------------------------------------------
// <copyright file="DeleteQuoteEvents.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Events;

using Bogsi.Quotable.Application.Models;

/// <summary>
///  Message that requests a delete of an existing quote.
/// </summary>
public sealed record DeleteQuoteRequestedEvent : BaseEvent
{
    /// <summary>
    /// Gets the quote model that needs to be deleted.
    /// </summary>
    required public Quote Model { get; init; }
}

/// <summary>
///  Message that triggers the delete of an existing quote.
/// </summary>
public sealed record DeleteQuoteEvent : BaseEvent
{
    /// <summary>
    /// Gets the quote model that needs to be deleted.
    /// </summary>
    required public Quote Model { get; init; }
}

/// <summary>
/// Event when a quote is deleted.
/// </summary>
public sealed record DeleteQuoteCompletedEvent : BaseEvent
{
}
