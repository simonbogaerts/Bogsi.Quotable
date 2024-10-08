using Bogsi.Quotable.Web.Extensions.Middleware;

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

internal static class RequestContextLoggingExtensions
{
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder application)
    {
        application.UseMiddleware<RequestContextLoggingMiddleware>();

        return application;
    }
}
