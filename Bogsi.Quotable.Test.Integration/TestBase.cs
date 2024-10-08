﻿using Bogsi.Quotable.Persistence;
using Bogsi.Quotable.Test.Integration.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bogsi.Quotable.Test.Integration;

public abstract class TestBase : IClassFixture<IntegrationTestWebApplicationBuilderFactory>, IDisposable
{
    private readonly IServiceScope _serviceScope;

    protected readonly QuotableContext _quotableContext;
    protected readonly HttpClient _client;

    public TestBase(IntegrationTestWebApplicationBuilderFactory factory)
    {
        _serviceScope = factory.Services.CreateScope();

        _quotableContext = _serviceScope.ServiceProvider.GetRequiredService<QuotableContext>();
        _quotableContext.Database.Migrate();

        _client = factory.CreateClient();
    }

    public void Dispose()
    {
        _quotableContext.Database.EnsureDeleted();
    }
}