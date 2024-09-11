using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

internal static class AuthExtensions
{
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
                                ValidIssuer = builder.Configuration[Constants.AppSettingKeys.ValidIssuer]!
                            };
                        });

        builder.Services.AddAuthorization();
    }
}

