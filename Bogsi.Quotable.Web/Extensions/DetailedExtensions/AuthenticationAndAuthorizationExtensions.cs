// -----------------------------------------------------------------------
// <copyright file="AuthenticationAndAuthorizationExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    internal static void AddAuthenticationAndAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(x =>
                        {
                            x.Audience = builder.Configuration[Constants.AppSettingKeys.Audience]!;
                            x.Authority = builder.Configuration[Constants.AppSettingKeys.Authority]!;
                            x.MetadataAddress = builder.Configuration[Constants.AppSettingKeys.MetadataAddress]!;
                            x.RequireHttpsMetadata = false;
                            x.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidIssuer = builder.Configuration[Constants.AppSettingKeys.ValidIssuer]!,
                            };
                        });

        builder.Services.AddAuthorization();
    }
}
