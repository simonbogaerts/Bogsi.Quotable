// -----------------------------------------------------------------------
// <copyright file="DistributedCacheExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using StackExchange.Redis;

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

/// <summary>
/// Extensions regarding database connections.
/// </summary>
internal static class DistributedCacheExtensions
{
    /// <summary>
    /// Configure and add Valkey support.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddDistributedCache(this WebApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString(Constants.ConnectionStrings.Valkey);

        ArgumentNullException.ThrowIfNullOrWhiteSpace(connectionString);

        builder.Services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        if (!builder.Environment.IsEnvironment(Constants.Environments.Testing))
        {
            var options = ConfigurationOptions.Parse(connectionString);

            options.AllowAdmin = true;

            ConnectionMultiplexer cm = ConnectionMultiplexer.Connect(options);

            builder.Services.AddSingleton<IConnectionMultiplexer>(cm);
        }
    }
}
