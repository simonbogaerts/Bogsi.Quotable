// -----------------------------------------------------------------------
// <copyright file="QuotableDbConfig.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Configs;

/// <summary>
/// Contains all the info for the QuotableDb connection.
/// </summary>
public sealed record QuotableDbConfig
{
    /// <summary>
    /// Gets the address of the database.
    /// </summary>
    required public string Server { get; init; }

    /// <summary>
    /// Gets the port of the database.
    /// </summary>
    required public int Port { get; init; }

    /// <summary>
    /// Gets the database name of the database.
    /// </summary>
    required public string DatabaseName { get; init; }

    /// <summary>
    /// Gets the username of the database.
    /// </summary>
    required public string UserName { get; init; }

    /// <summary>
    /// Gets the password of the database.
    /// </summary>
    required public string Password { get; init; }

    /// <summary>
    /// Gets the connectionstring of the database.
    /// </summary>
    public string ConnectionString =>
        $"Server={Server};Port={Port};Database={DatabaseName};User Id={UserName};Password={Password};";
}
