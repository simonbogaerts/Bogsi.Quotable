using Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Serilog;

namespace Bogsi.Quotable.Web.Extensions;

internal static class WebApplicationExtensions
{
    internal static void ConfigureWebApplication(this WebApplication application)
    {
        application.UseRequestContextLogging();
        application.UseHttpsRedirection();
        application.UseSerilogRequestLogging();
        application.UseRouting();
        application.UseCors();
        application.UseAuthentication();
        application.UseAuthorization();
        application.UseVersionedApiEndpoints();
        application.UseSwagger();
        application.UseSwaggerUI();
    }
}
