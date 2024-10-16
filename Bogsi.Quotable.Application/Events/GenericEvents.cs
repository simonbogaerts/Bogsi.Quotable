// -----------------------------------------------------------------------
// <copyright file="GenericEvents.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Events;

using Bogsi.Quotable.Application.Enums;

/// <summary>
/// Event when audit dates are updated.
/// </summary>
public sealed record UpdateAuditEntityEvent : BaseEvent
{
    /// <summary>
    /// Gets the type of audit update required.
    /// </summary>
    public AuditUpdateType Type { get; init; }
}

/// <summary>
/// Event when audit dates are updated.
/// </summary>
public sealed record AuditEntityUpdatedEvent : BaseEvent
{
    /// <summary>
    /// Gets a value indicating whether the UpdateAuditEntityEvent was succesful.
    /// </summary>
    public bool IsSuccess { get; init; }
}

/// <summary>
/// Event when audit dates are updated.
/// </summary>
public sealed record CleanCacheEvent : BaseEvent
{
}

/// <summary>
/// Event when audit dates are updated.
/// </summary>
public sealed record CacheCleanedEvent : BaseEvent
{
}

/// <summary>
/// Event when something goes wrong to finalize the saga..
/// </summary>
public sealed record FinalizeFailedEvent : BaseEvent
{
}
