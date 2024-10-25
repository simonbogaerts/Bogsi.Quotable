// -----------------------------------------------------------------------
// <copyright file="Value.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Constants.Properties;

/// <summary>
/// Static values regarding the Value property of the Quote model.
/// </summary>
public sealed record Value
{
    /// <summary>
    /// Gets minimum length value.
    /// </summary>
    public const int MinimumLength = 5;

    /// <summary>
    /// Gets maximum lentgh value.
    /// </summary>
    public const int MaximumLength = 1255;
}
