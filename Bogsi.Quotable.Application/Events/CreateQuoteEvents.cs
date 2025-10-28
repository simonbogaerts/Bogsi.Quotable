// -----------------------------------------------------------------------
// <copyright file="CreateQuoteEvents.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Events;

/// <summary>
///  Message that requests the creation of a new quote.
/// </summary>
public sealed record CreateQuoteRequestedEvent : BaseEvent
{
    /// <summary>
    /// Gets the quote model that needs to be created.
    /// </summary>
    required public Quote Model { get; init; }
}

/// <summary>
///  Message that triggers the creation of a new quote.
/// </summary>
public sealed record CreateQuoteEvent : BaseEvent
{
    /// <summary>
    /// Gets the quote model that needs to be created.
    /// </summary>
    required public Quote Model { get; init; }
}

/// <summary>
/// Event when a quote is created.
/// </summary>
public sealed record CreateQuoteCompletedEvent : BaseEvent
{
    /// <summary>
    /// Gets the quote model that has been created.
    /// </summary>
    required public Quote Model { get; init; }
}
