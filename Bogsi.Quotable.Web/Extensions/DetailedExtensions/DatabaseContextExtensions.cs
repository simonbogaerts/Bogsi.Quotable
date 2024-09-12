using Bogsi.Quotable.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

internal static class DatabaseContextExtensions
{
    internal static void AddQuotableDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<QuotableContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString(Constants.QuotableDb)!);
        });

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    }
}
