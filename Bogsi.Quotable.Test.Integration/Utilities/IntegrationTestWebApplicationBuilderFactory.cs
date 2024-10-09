namespace Bogsi.Quotable.Test.Integration.Utilities;

using Bogsi.Quotable.Persistence;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Testcontainers.PostgreSql;

public class IntegrationTestWebApplicationBuilderFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _quotableContainer = new PostgreSqlBuilder()
        .WithImage(Constants.Database.Image)
        .WithDatabase(Constants.Database.Name)
        .WithUsername(Constants.Database.User)
        .WithPassword(Constants.Database.Password)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(Constants.Environments.Testing);

        builder.ConfigureTestServices(services => 
        {
            var descriptor = services
                .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<QuotableContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<QuotableContext>(options =>
            {
                options.UseNpgsql(_quotableContainer.GetConnectionString());
            });
        });
    }

    public Task InitializeAsync()
    {
        return _quotableContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _quotableContainer.StopAsync();
    }
}
