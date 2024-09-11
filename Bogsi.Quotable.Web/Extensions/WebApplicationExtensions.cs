using Bogsi.Quotable.Web.Extensions.DetailedExtensions;

namespace Bogsi.Quotable.Web.Extensions;

internal static class WebApplicationExtensions
{
    internal static void ConfigureWebApplication(this WebApplication application)
    {
        application.UseHttpsRedirection();
        application.UseRouting();
        application.UseAuthentication();
        application.UseAuthorization();
        application.UseVersionedApiEndpoints();
        application.UseSwagger();
        application.UseSwaggerUI();
    }
}
