// -----------------------------------------------------------------------
// <copyright file="DatabaseContextExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Bogsi.Quotable.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

/// <summary>
/// Extensions regarding database connections.
/// </summary>
internal static class DatabaseContextExtensions
{
    /// <summary>
    /// Configure and add DbContext.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddQuotableDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<QuotableContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString(Common.Constants.ConnectionStringKey.QuotableDb)!,
                o => o.MigrationsHistoryTable(
                    HistoryRepository.DefaultTableName,
                    Common.Constants.Database.Schemas.Saga)));

        builder.Services.AddDbContext<SagaContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString(Common.Constants.ConnectionStringKey.QuotableDb)!,
                o => o.MigrationsHistoryTable(
                    HistoryRepository.DefaultTableName,
                    Common.Constants.Database.Schemas.Saga)));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    }
}
