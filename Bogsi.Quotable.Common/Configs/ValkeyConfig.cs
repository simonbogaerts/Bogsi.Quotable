// -----------------------------------------------------------------------
// <copyright file="ValkeyConfig.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Configs;

/// <summary>
/// Contains all the info for the cache server.
/// </summary>
public sealed record ValkeyConfig
{
    /// <summary>
    /// Gets the address of the cache server.
    /// </summary>
    required public string Address { get; init; }

    /// <summary>
    /// Gets the port of the cache server.
    /// </summary>
    required public int Port { get; init; }

    /// <summary>
    /// Gets the connectionstring of the cache server.
    /// </summary>
    public string ConnectionString => $"{Address}:{Port}";
}
