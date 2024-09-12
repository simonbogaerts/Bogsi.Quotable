using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Bogsi.Quotable.Persistence;

internal sealed class QuotableContextFactory : IDesignTimeDbContextFactory<QuotableContext>
{
    public QuotableContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<QuotableContextFactory>()
            .Build();

        string connectionString = configuration.GetConnectionString(Constants.QuotableDb)!; 

        var builder = new DbContextOptionsBuilder<QuotableContext>()
            .UseNpgsql(connectionString)
            .EnableSensitiveDataLogging(); 

        return new QuotableContext(builder.Options);
    }
}
