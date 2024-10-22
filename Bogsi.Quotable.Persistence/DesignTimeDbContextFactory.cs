// -----------------------------------------------------------------------
// <copyright file="DesignTimeDbContextFactory.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Factory to create and update a database based upon a database context and entity configuration.
/// </summary>
/// <typeparam name="T">Class implementing DbContext.</typeparam>
internal abstract class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T>
    where T : DbContext
{
    /// <summary>
    /// Method to create a new DbContext.
    /// </summary>
    /// <param name="args">Arguments to pass into the factory.</param>
    /// <returns>A new database context.</returns>
    public T CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<DesignTimeDbContextFactory<T>>()
            .Build();

        string connectionString = configuration.GetConnectionString(Common.Constants.Database.DatabaseNames.QuotableDb)!;

        var builder = GetBuilder(connectionString);

        var dbContext = Activator.CreateInstance(
            typeof(T),
            builder.Options) as T;

        return dbContext!;
    }

    /// <summary>
    /// Configure the builder for the specific contexts.
    /// </summary>
    /// <param name="connectionString">The connection to the quotable-db instance.</param>
    /// <returns>Builder configured for context (particularly the migration table).</returns>
    protected abstract DbContextOptionsBuilder<T> GetBuilder(string connectionString);
}

/// <summary>
/// Factory to create and update QuotableContext.
/// Add-Migration [migration-name] -Context QuotableContext -o Migrations/Quotable.
/// </summary>
internal sealed class QuotableContextFactory : DesignTimeDbContextFactory<QuotableContext>
{
    /// <inheritdoc/>
    protected override DbContextOptionsBuilder<QuotableContext> GetBuilder(string connectionString)
    {
        var options = new DbContextOptionsBuilder<QuotableContext>()
            .UseNpgsql(
                connectionString,
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Common.Constants.Database.Schemas.Quotable))
            .EnableSensitiveDataLogging();

        return options;
    }
}

/// <summary>
/// Factory to create and update SagaContext.
/// Add-Migration [migration-name] -Context SagaContext -o Migrations/Saga.
/// </summary>
internal sealed class SagaContextFactory : DesignTimeDbContextFactory<SagaContext>
{
    /// <inheritdoc/>
    protected override DbContextOptionsBuilder<SagaContext> GetBuilder(string connectionString)
    {
        var options = new DbContextOptionsBuilder<SagaContext>()
            .UseNpgsql(
                connectionString,
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Common.Constants.Database.Schemas.Saga))
            .EnableSensitiveDataLogging();

        return options;
    }
}
