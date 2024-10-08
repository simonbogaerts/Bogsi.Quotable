using Bogsi.Quotable.Web.Extensions.DetailedExtensions;

namespace Bogsi.Quotable.Web.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static void ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.AddLoggingWithSerilogAndSeq();
        builder.AddApiExplorerWithVersioning();
        builder.AddAuthenticationAndAuthorization();
        builder.AddSwaggerGenWithAuth();
        builder.AddApiEndpoints();
        builder.AddQuotableDbContext();
        builder.AddServices();
    }
}