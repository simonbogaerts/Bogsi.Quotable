// -----------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Bogsi.Quotable.Application;
using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Infrastructure.Repositories;
using Bogsi.Quotable.Infrastructure.Utilities;

using FluentValidation;

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
        builder.Services.AddHealthChecks();
        builder.Services.AddAutoMapper(typeof(IApplicationMarker).Assembly);
        builder.Services.AddValidatorsFromAssembly(typeof(IApplicationMarker).Assembly);
        builder.Services.AddCors();

        builder.AddRepositories();
        builder.AddUtilities();
        builder.AddHandlers();
    }

    /// <summary>
    /// Configure and add all repositories.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    private static void AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<QuoteRepository>()
            .AddScoped<IReadonlyRepository<Quote>>(x => x.GetRequiredService<QuoteRepository>())
            .AddScoped<IRepository<Quote>>(x => x.GetRequiredService<QuoteRepository>());
    }

    /// <summary>
    /// Configure and add all handlers.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    private static void AddHandlers(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IGetQuotesHandler, GetQuotesHandler>();
        builder.Services.AddScoped<IGetQuoteByIdHandler, GetQuoteByIdHandler>();
        builder.Services.AddScoped<ICreateQuoteHandler, CreateQuoteHandler>();
        builder.Services.AddScoped<IUpdateQuoteHandler, UpdateQuoteHandler>();
        builder.Services.AddScoped<IDeleteQuoteHandler, DeleteQuoteHandler>();
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
