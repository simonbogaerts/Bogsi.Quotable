// -----------------------------------------------------------------------
// <copyright file="HealthCheckExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Bogsi.Quotable.Persistence;

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
        string? databaseConnectionString = builder.Configuration.GetConnectionString(Constants.ConnectionStrings.QuotableDb);

        ArgumentNullException.ThrowIfNullOrWhiteSpace(databaseConnectionString);

        string? valKeyConnectionString = builder.Configuration.GetConnectionString(Constants.ConnectionStrings.Valkey);

        ArgumentNullException.ThrowIfNullOrWhiteSpace(valKeyConnectionString);

        builder.Services
            .AddHealthChecks()
            .AddNpgSql(databaseConnectionString)
            .AddRedis(valKeyConnectionString);
    }
}
