// -----------------------------------------------------------------------
// <copyright file="EnvironmentVariable.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Constants;

/// <summary>
/// Different environment variables.
/// </summary>
public sealed record EnvironmentVariable
{
    /// <summary>
    /// Production environment.
    /// </summary>
    public const string ApsNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
}
