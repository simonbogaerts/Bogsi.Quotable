namespace Bogsi.Quotable.Test.Integration.Utilities.TestServiceConfiguration;

using Bogsi.Quotable.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

internal static class QuotableContextTestConfiguration
{
    internal static void ConfigureQuotableContextForIntegrationTest(this IServiceCollection services, string connectionString)
    {
        var descriptor = services
            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<QuotableContext>));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<QuotableContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }
}
