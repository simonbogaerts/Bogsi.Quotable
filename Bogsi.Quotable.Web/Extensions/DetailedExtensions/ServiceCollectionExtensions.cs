using Bogsi.Quotable.Application;
using Bogsi.Quotable.Application.Handlers.Quotes.GetQuoteByIdHandler;
using Bogsi.Quotable.Application.Handlers.Quotes.GetQuotes;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Infrastructure.Repositories;

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

internal static class ServiceCollectionExtensions
{
    internal static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();
        builder.Services.AddAutoMapper(typeof(IApplicationMarker).Assembly);

        builder.AddRepositories();
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
    }
}
