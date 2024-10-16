// -----------------------------------------------------------------------
// <copyright file="CreateQuoteEvents.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Events;

/// <summary>
///  Message that triggers the creation of a new quote.
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
public sealed record QuoteCreatedEvent : BaseEvent
{
    /// <summary>
    /// Gets a value indicating whether the CreateQuoteEvent was succesful.
    /// </summary>
    public bool IsSuccess { get; init; }
}

/// <summary>
/// Event when audit dates are updated.
/// </summary>
public sealed record CreateQuoteCompletedEvent : BaseEvent
{
}
