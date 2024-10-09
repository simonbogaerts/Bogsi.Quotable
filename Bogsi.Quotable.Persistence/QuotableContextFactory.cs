// -----------------------------------------------------------------------
// <copyright file="QuotableContextFactory.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Factory to create and update a database based upon a database context and entity configuration.
/// </summary>
internal sealed class QuotableContextFactory : IDesignTimeDbContextFactory<QuotableContext>
{
    /// <summary>
    /// Method to create a new DbContext.
    /// </summary>
    /// <param name="args">Arguments to pass into the factory.</param>
    /// <returns>A new database context.</returns>
    public QuotableContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<QuotableContextFactory>()
            .Build();

        string connectionString = configuration.GetConnectionString(Constants.QuotableDb)!; 

        var builder = new DbContextOptionsBuilder<QuotableContext>()
            .UseNpgsql(connectionString)
            .EnableSensitiveDataLogging();

        return new QuotableContext(builder.Options);
    }
}
