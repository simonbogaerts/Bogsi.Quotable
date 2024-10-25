// -----------------------------------------------------------------------
// <copyright file="AuthenticationAndAuthorizationExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Modules.Extensions;

using Bogsi.Quotable.Common.Enums;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Extensions authorization and authentication.
/// </summary>
internal static class AuthExtensions
{
    /// <summary>
    /// Configure and add authorization and authentication.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddAndConfigureAuth(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var authConfig = builder.GetOrAddAuthConfig(ServiceCollectionOptions.Return);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(x =>
                        {
                            x.Audience = authConfig.Audience;
                            x.Authority = authConfig.Authority;
                            x.MetadataAddress = authConfig.MetadataAddress;
                            x.RequireHttpsMetadata = false;
                            x.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidIssuer = authConfig.ValidIssuer,
                            };
                        });

        builder.Services.AddAuthorization();
    }
}
