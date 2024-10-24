// -----------------------------------------------------------------------
// <copyright file="AuthConfig.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Configs;

/// <summary>
/// Contains all the info for Authentication and Authorization.
/// </summary>
public sealed record AuthConfig
{
    /// <summary>
    /// Gets the AuthorizationUrl.
    /// </summary>
    required public Uri AuthorizationUrl { get; init; }

    /// <summary>
    /// Gets the MetadataAddress.
    /// </summary>
    required public string MetadataAddress { get; init; }

    /// <summary>
    /// Gets the ValidIssuer.
    /// </summary>
    required public string ValidIssuer { get; init; }

    /// <summary>
    /// Gets the Authority.
    /// </summary>
    required public string Authority { get; init; }

    /// <summary>
    /// Gets the Audience.
    /// </summary>
    required public string Audience { get; init; }
}
