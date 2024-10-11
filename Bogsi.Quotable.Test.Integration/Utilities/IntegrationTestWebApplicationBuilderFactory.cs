namespace Bogsi.Quotable.Test.Integration.Utilities;

using Bogsi.Quotable.Test.Integration.Utilities.TestServiceConfiguration;

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

using Testcontainers.PostgreSql;

public class IntegrationTestWebApplicationBuilderFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _quotableContainer = new PostgreSqlBuilder()
        .WithImage(Constants.Database.Image)
        .WithDatabase(Constants.Database.Name)
        .WithUsername(Constants.Database.User)
        .WithPassword(Constants.Database.Password)
        .Build();

    private readonly IContainer _valkeyContainer = new ContainerBuilder()
        .WithImage(Constants.Valkey.Image)
        .WithPortBinding(Constants.Valkey.Port, true)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(Web.Constants.Environments.Testing);

        builder.ConfigureTestServices(services => 
        {
            services.ConfigureQuotableContextForIntegrationTest(_quotableContainer.GetConnectionString());
            services.ConfigureValkeyForIntegrationTest($"{_valkeyContainer.Hostname}:{_valkeyContainer.GetMappedPublicPort(6379)}");
        });
    }

    public async Task InitializeAsync()
    {
        await _quotableContainer.StartAsync();
        await _valkeyContainer.StartAsync();
    }

    public async new Task DisposeAsync()
    {
        await _quotableContainer.StopAsync();
        await _valkeyContainer.StopAsync();
    }
}
