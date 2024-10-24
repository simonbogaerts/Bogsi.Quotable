// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionOptions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Enums;

/// <summary>
/// Options for adding a config to IServiceCollection.
/// </summary>
public enum ServiceCollectionOptions
{
    /// <summary>
    /// Add and return singleton.
    /// </summary>
    AddAndReturn,

    /// <summary>
    /// Only return singleton.
    /// </summary>
    Return,
}
