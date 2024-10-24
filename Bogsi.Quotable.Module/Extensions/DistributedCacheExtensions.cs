// -----------------------------------------------------------------------
// <copyright file="DistributedCacheExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Modules.Extensions;

using Bogsi.Quotable.Common.Enums;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StackExchange.Redis;

/// <summary>
/// Extensions regarding database connections.
/// </summary>
internal static class DistributedCacheExtensions
{
    /// <summary>
    /// Configure and add Valkey support.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddAndConfigureDistributedCache(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var cachingConfig = builder.GetOrAddValkeyConfig(ServiceCollectionOptions.Return);

        builder.Services.AddStackExchangeRedisCache(options => options.Configuration = cachingConfig.ConnectionString);

        if (!builder.Environment.IsEnvironment(Common.Constants.Environment.Testing))
        {
            var options = ConfigurationOptions.Parse(cachingConfig.ConnectionString);

            options.AllowAdmin = true;
            options.AbortOnConnectFail = false;

            ConnectionMultiplexer cm = ConnectionMultiplexer.Connect(options);

            builder.Services.AddSingleton<IConnectionMultiplexer>(cm);
        }
    }
}
