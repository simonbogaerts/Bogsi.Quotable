// -----------------------------------------------------------------------
// <copyright file="ConnectionStringKey.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Constants;

/// <summary>
/// Different connecting string keys (appsettings.json).
/// </summary>
public sealed record ConnectionStringKey
{
    /// <summary>
    /// Name of the database connectionstring.
    /// </summary>
    public const string QuotableDb = nameof(QuotableDb);

    /// <summary>
    /// Name of the distributed cache connectionstring.
    /// </summary>
    public const string Valkey = nameof(Valkey);
}
