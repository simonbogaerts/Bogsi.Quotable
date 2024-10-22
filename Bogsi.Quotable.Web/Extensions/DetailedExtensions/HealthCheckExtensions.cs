// -----------------------------------------------------------------------
// <copyright file="HealthCheckExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

/// <summary>
/// Extensions regarding health checks.
/// </summary>
internal static class HealthCheckExtensions
{
    /// <summary>
    /// Add and configure all health checks.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddAndConfigureHealthChecks(this WebApplicationBuilder builder)
    {
        string? databaseConnectionString = builder.Configuration.GetConnectionString(Common.Constants.ConnectionStringKey.QuotableDb);

        ArgumentNullException.ThrowIfNullOrWhiteSpace(databaseConnectionString);

        string? valKeyConnectionString = builder.Configuration.GetConnectionString(Common.Constants.ConnectionStringKey.Valkey);

        ArgumentNullException.ThrowIfNullOrWhiteSpace(valKeyConnectionString);

        // Fix this when redoing DI.
        string rabbitMq = "amqp://quotable:quotable@localhost:5672";

        ArgumentNullException.ThrowIfNullOrWhiteSpace(nameof(rabbitMq));

        builder.Services
            .AddHealthChecks()
            .AddNpgSql(databaseConnectionString)
            .AddRedis(valKeyConnectionString)
            .AddRabbitMQ(new Uri(rabbitMq));
    }
}
