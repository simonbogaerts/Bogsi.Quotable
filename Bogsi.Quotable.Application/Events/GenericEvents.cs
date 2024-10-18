// -----------------------------------------------------------------------
// <copyright file="GenericEvents.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Events;

using Bogsi.Quotable.Application.Enums;
using Bogsi.Quotable.Application.Models;

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

/// <summary>
/// Event when the saga is done/completed.
/// </summary>
public sealed record QuoteSagaCompletedEvent : BaseEvent
{
}

/// <summary>
/// Event when something goes wrong to finalize the saga.
/// </summary>
public sealed record FinalizeFailedSagaEvent : BaseEvent
{
}

/// <summary>
/// Event when you want to do something with the cache.
/// </summary>
public sealed record CacheMaintanenceRequestedEvent : BaseEvent
{
    /// <summary>
    /// Gets cache maintenance type.
    /// </summary>
    public CacheMaintenanceType MaintenanceType { get; init; }

    /// <summary>
    /// Gets the quote model that needs to be cached.
    /// </summary>
    public Quote? Model { get; init; }
}

/// <summary>
/// Event when cache maintenance is done.
/// </summary>
public sealed record CacheMaintenanceCompletedEvent : BaseEvent
{
}
