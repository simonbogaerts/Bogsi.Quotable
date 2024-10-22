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

            x.AddSecurityDefinition(Common.Constants.Security.Keycloak, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(builder.Configuration[Common.Constants.AuthenticationKeys.AuthorizationUrl]!),
                        Scopes = new Dictionary<string, string>
                        {
                            { Common.Constants.Security.Scopes.OpenId, Common.Constants.Security.Scopes.OpenId },
                            { Common.Constants.Security.Scopes.Profile, Common.Constants.Security.Scopes.Profile },
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
                            Id = Common.Constants.Security.Keycloak,
                            Type = ReferenceType.SecurityScheme,
                        },
                        In = ParameterLocation.Header,
                        Name = Common.Constants.Security.Bearer,
                        Scheme = Common.Constants.Security.Bearer,
                    },
                    []
                },
            };

            x.AddSecurityRequirement(securityRequirement);
        });
    }
}
