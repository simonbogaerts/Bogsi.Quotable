namespace Bogsi.Quotable.Test.Integration.Utilities.TestServiceConfiguration;

using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

internal static class ValkeyTestConfiguration
{
    internal static void ConfigureValkeyForIntegrationTest(this IServiceCollection services, string connectionString)
    {
        var descriptor = services
            .SingleOrDefault(service => service.ServiceType == typeof(RedisCacheOptions));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionString;
        });

        var options = ConfigurationOptions.Parse(connectionString);
        options.AllowAdmin = true;

        ConnectionMultiplexer cm = ConnectionMultiplexer.Connect(options);
        services.AddSingleton<IConnectionMultiplexer>(cm);
    }
}
