// -----------------------------------------------------------------------
// <copyright file="Environment.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Constants;

/// <summary>
/// Different environments.
/// </summary>
public sealed record Environment
{
    /// <summary>
    /// Production environment.
    /// </summary>
    public const string Production = nameof(Production);

    /// <summary>
    /// Test environment.
    /// </summary>
    public const string Testing = nameof(Testing);

    /// <summary>
    /// Test environment.
    /// </summary>
    public const string Development = nameof(Development);
}
