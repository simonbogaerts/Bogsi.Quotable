// -----------------------------------------------------------------------
// <copyright file="Cursor.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Constants.Properties;

/// <summary>
/// Static values regarding cursor.
/// </summary>
public sealed record Cursor
{
    /// <summary>
    /// Gets none or empty value.
    /// </summary>
    public const int None = 0;

    /// <summary>
    /// Gets minimum value.
    /// </summary>
    public const int Minimum = 1;

    /// <summary>
    /// Gets default value.
    /// </summary>
    public const int Default = 1;

    /// <summary>
    /// Gets offset (required for calculating new cursor).
    /// </summary>
    public const int Offset = 1;
}
