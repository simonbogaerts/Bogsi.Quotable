using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Handlers.Quotes.GetQuotes;

public interface IGetQuotesHandler
{
    Task<GetQuotesHandlerResponse> HandleAsync(
        GetQuotesHandlerRequest request, 
        CancellationToken cancellationToken);
}

public sealed class GetQuotesHandler : IGetQuotesHandler
{
    private readonly IReadonlyRepository<Quote> _quoteRepository;

    public GetQuotesHandler(IReadonlyRepository<Quote> quoteRepository)
    {
        _quoteRepository = quoteRepository;
    }

    public Task<GetQuotesHandlerResponse> HandleAsync(
        GetQuotesHandlerRequest request, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
