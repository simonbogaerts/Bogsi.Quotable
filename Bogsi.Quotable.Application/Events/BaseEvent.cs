// -----------------------------------------------------------------------
// <copyright file="BaseEvent.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Events;

/// <summary>
/// Base event type.
/// </summary>
public abstract record BaseEvent
{
    /// <summary>
    /// Gets the public identifier of an item.
    /// </summary>
    public Guid PublicId { get; init; }
}
