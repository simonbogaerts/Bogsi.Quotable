// -----------------------------------------------------------------------
// <copyright file="DatabaseContextExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Bogsi.Quotable.Persistence;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Extensions regarding database connections.
/// </summary>
internal static class DatabaseContextExtensions
{
    /// <summary>
    /// Configure and add DbCContext.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddQuotableDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<QuotableContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString(Constants.QuotableDb)!);
        });

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    }
}
