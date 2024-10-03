using Serilog;

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

internal static class LoggingExtensions
{
    internal static void AddLoggingWithSerilogAndSeq(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));
    }
}
