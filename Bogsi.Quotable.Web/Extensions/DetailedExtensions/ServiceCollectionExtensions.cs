using Bogsi.Quotable.Application;
using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Infrastructure.Repositories;
using Bogsi.Quotable.Infrastructure.Utilities;

using FluentValidation;

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

internal static class ServiceCollectionExtensions
{
    internal static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();
        builder.Services.AddAutoMapper(typeof(IApplicationMarker).Assembly);
        builder.Services.AddValidatorsFromAssembly(typeof(IApplicationMarker).Assembly);

        builder.AddRepositories();
        builder.AddUtilities();
        builder.AddHandlers();
    }

    private static void AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<QuoteRepository>()
            .AddScoped<IReadonlyRepository<Quote>>(x => x.GetRequiredService<QuoteRepository>())
            .AddScoped<IRepository<Quote>>(x => x.GetRequiredService<QuoteRepository>());
    }

    private static void AddHandlers(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IGetQuotesHandler, GetQuotesHandler>();
        builder.Services.AddScoped<IGetQuoteByIdHandler, GetQuoteByIdHandler>();
        builder.Services.AddScoped<ICreateQuoteHandler, CreateQuoteHandler>();
        builder.Services.AddScoped<IUpdateQuoteHandler, UpdateQuoteHandler>();
        builder.Services.AddScoped<IDeleteQuoteHandler, DeleteQuoteHandler>();
    }

    private static void AddUtilities(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
