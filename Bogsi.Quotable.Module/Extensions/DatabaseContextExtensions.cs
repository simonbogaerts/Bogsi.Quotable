// -----------------------------------------------------------------------
// <copyright file="DatabaseContextExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Modules.Extensions;

using Bogsi.Quotable.Common.Enums;
using Bogsi.Quotable.Persistence;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Test.
/// </summary>
internal static class DatabaseContextExtensions
{
    /// <summary>
    /// Add and configure all database contexts.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddAndConfigureDatabaseContexts(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var databaseConfig = builder.GetOrAddQuotableDbConfig(ServiceCollectionOptions.Return);

        builder.Services.AddDbContext<QuotableContext>(options =>
            options.UseNpgsql(
                databaseConfig.ConnectionString,
                o => o.MigrationsHistoryTable(
                    HistoryRepository.DefaultTableName,
                    Common.Constants.Database.Schemas.Quotable)));

        builder.Services.AddDbContext<SagaContext>(options =>
            options.UseNpgsql(
                databaseConfig.ConnectionString,
                o => o.MigrationsHistoryTable(
                    HistoryRepository.DefaultTableName,
                    Common.Constants.Database.Schemas.Saga)));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    }
}
