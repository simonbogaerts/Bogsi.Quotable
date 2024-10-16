// -----------------------------------------------------------------------
// <copyright file="AuditUpdateType.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Enums;

/// <summary>
/// Defines the types of updates that can be done on Auditable items.
/// </summary>
public enum AuditUpdateType
{
    /// <summary>
    /// Updates both the Created and Updated dates.
    /// </summary>
    Create,

    /// <summary>
    /// Updates the Updated date.
    /// </summary>
    Update,
}
