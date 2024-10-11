namespace Bogsi.Quotable.Test.Integration;

using Bogsi.Quotable.Persistence;
using Bogsi.Quotable.Test.Integration.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

public abstract class TestBase : IClassFixture<IntegrationTestWebApplicationBuilderFactory>, IDisposable
{
    private readonly IServiceScope _serviceScope;

    protected readonly QuotableContext _quotableContext;
    protected readonly IServer _cache;
    protected readonly HttpClient _client;

    public TestBase(IntegrationTestWebApplicationBuilderFactory factory)
    {
        _serviceScope = factory.Services.CreateScope();

        _quotableContext = _serviceScope.ServiceProvider.GetRequiredService<QuotableContext>();
        _quotableContext.Database.Migrate();

        var muxer = _serviceScope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
        _cache = muxer.GetServers().First();

        _client = factory.CreateClient();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        CleanUpContext();
        CleanUpCache();
    }

    private void CleanUpContext()
    {
        _quotableContext.Database.EnsureDeleted();
    }

    private void CleanUpCache()
    {
        _cache.FlushAllDatabases();
    }
}
