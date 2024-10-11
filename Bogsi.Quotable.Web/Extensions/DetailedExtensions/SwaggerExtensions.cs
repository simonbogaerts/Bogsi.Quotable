// -----------------------------------------------------------------------
// <copyright file="SwaggerExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Microsoft.OpenApi.Models;

/// <summary>
/// Extensions regarding swagger.
/// </summary>
internal static class SwaggerExtensions
{
    /// <summary>
    /// Configure and add swagger to configured services.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddSwaggerGenWithAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

            x.AddSecurityDefinition(Constants.Keycloak, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(builder.Configuration[Constants.AppSettingKeys.AuthorizationUrl]!),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "openid" },
                            { "profile", "profile" },
                        },
                    },
                },
            });

            OpenApiSecurityRequirement securityRequirement = new ()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = Constants.Keycloak,
                            Type = ReferenceType.SecurityScheme,
                        },
                        In = ParameterLocation.Header,
                        Name = Constants.Bearer,
                        Scheme = Constants.Bearer,
                    },
                    []
                },
            };

            x.AddSecurityRequirement(securityRequirement);
        });
    }
}