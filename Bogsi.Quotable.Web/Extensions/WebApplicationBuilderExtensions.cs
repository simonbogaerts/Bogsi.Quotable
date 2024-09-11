using Bogsi.Quotable.Web.Extensions.DetailedExtensions;

namespace Bogsi.Quotable.Web.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static void ConfigureBuilder(this WebApplicationBuilder builder)
    {
        // IServiceCollection configuration
        builder.Services.AddHealthChecks();

        // IWebApplicationBuilder configuration
        builder.AddApiExplorerWithVersioning();
        builder.AddAuthenticationAndAuthorization();
        builder.AddSwaggerGenWithAuth();
        builder.AddApiEndpoints();
        builder.AddServices();
    }
}