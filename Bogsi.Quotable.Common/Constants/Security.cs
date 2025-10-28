// -----------------------------------------------------------------------
// <copyright file="Security.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Constants;

/// <summary>
/// Constants regarding security.
/// </summary>
public sealed record Security
{
    /// <summary>
    /// Name of the keycloak section.
    /// </summary>
    public const string Keycloak = nameof(Keycloak);

    /// <summary>
    /// Name of the authentication schema.
    /// </summary>
    public const string Bearer = nameof(Bearer);

    /// <summary>
    /// Different scopes within OAuth.
    /// </summary>
    public sealed record Scopes
    {
        /// <summary>
        /// Scope: openid.
        /// </summary>
        public const string OpenId = "openid";

        /// <summary>
        /// Scope: profile.
        /// </summary>
        public const string Profile = "profile";
    }
}
