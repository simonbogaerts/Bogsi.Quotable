// -----------------------------------------------------------------------
// <copyright file="Unit.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Models;

/// <summary>
/// Void typed response used during Result Pattern.
/// </summary>
public readonly struct Unit
{
    /// <summary>
    /// Gets default void value.
    /// Server as a flyweight.
    /// </summary>
    public static Unit Instance => default;
}
