// -----------------------------------------------------------------------
// <copyright file="HealthCheckExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Modules.Extensions;

using Bogsi.Quotable.Common.Enums;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions regarding message queue.
/// </summary>
internal static class HealthCheckExtensions
{
    /// <summary>
    /// Add and configure all health checks.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddAndConfigureHealthChecks(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var databaseConfig = builder.GetOrAddQuotableDbConfig(ServiceCollectionOptions.Return);
        var messagingConfig = builder.GetOrAddMassTransitConfig(ServiceCollectionOptions.Return);
        var cachingConfig = builder.GetOrAddValkeyConfig(ServiceCollectionOptions.Return);

        builder.Services
            .AddHealthChecks()
            .AddNpgSql(databaseConfig.ConnectionString)
            .AddRabbitMQ(new Uri(messagingConfig.ConnectionString))
            .AddRedis(cachingConfig.ConnectionString);
    }
}
