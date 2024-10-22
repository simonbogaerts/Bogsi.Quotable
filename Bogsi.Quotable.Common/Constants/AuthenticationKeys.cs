// -----------------------------------------------------------------------
// <copyright file="AuthenticationKeys.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Constants;

/// <summary>
/// Appsetting keys regarding the Authenticatio segment.
/// </summary>
public sealed record AuthenticationKeys
{
    /// <summary>
    /// gets audience for auth.
    /// </summary>
    public const string Audience = "Authentication:Audience";

    /// <summary>
    /// gets authority for auth.
    /// </summary>
    public const string Authority = "Authentication:Authority";

    /// <summary>
    /// gets metadata address for auth.
    /// </summary>
    public const string MetadataAddress = "Authentication:MetadataAddress";

    /// <summary>
    /// gets valid issuer for auth.
    /// </summary>
    public const string ValidIssuer = "Authentication:ValidIssuer";

    /// <summary>
    /// gets authorization url for auth.
    /// </summary>
    public const string AuthorizationUrl = "Keycloak:AuthorizationUrl";
}
