// -----------------------------------------------------------------------
// <copyright file="AppSettingSections.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Constants;

/// <summary>
/// Constants regarding security.
/// </summary>
public sealed record AppSettingSections
{
    /// <summary>
    /// Name of the Auth section.
    /// </summary>
    public const string Auth = nameof(Auth);

    /// <summary>
    /// Name of the QuotableDb section.
    /// </summary>
    public const string QuotableDb = nameof(QuotableDb);

    /// <summary>
    /// Name of the Messaging section.
    /// </summary>
    public const string Messaging = nameof(Messaging);

    /// <summary>
    /// Name of the Valkey section.
    /// </summary>
    public const string Valkey = nameof(Valkey);
}
