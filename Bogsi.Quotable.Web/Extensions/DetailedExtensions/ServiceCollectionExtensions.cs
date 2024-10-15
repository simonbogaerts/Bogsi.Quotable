// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Bogsi.Quotable.Application;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Infrastructure.Repositories;
using Bogsi.Quotable.Infrastructure.Utilities;

using FluentValidation;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions regarding service collexction.
/// </summary>
internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add and configure all services.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(IApplicationMarker).Assembly);
        builder.Services.AddValidatorsFromAssembly(typeof(IApplicationMarker).Assembly);
        builder.Services.AddCors();

        builder.AddRepositories();
        builder.AddUtilities();
    }

    /// <summary>
    /// Configure and add all repositories.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    private static void AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<QuoteRepository>();
        builder.Services
            .AddScoped<IReadonlyRepository<Quote>>(x =>
                new CachedQuoteRepository(x.GetRequiredService<QuoteRepository>(), x.GetRequiredService<IDistributedCache>()))
            .AddScoped<IRepository<Quote>>(x =>
                new CachedQuoteRepository(x.GetRequiredService<QuoteRepository>(), x.GetRequiredService<IDistributedCache>()));
    }

    /// <summary>
    /// Configure and add utility services.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    private static void AddUtilities(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
