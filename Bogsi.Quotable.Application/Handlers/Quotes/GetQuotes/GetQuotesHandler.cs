using AutoMapper;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Handlers.Quotes.GetQuotes;

public interface IGetQuotesHandler
{
    Task<GetQuotesHandlerResponse> HandleAsync(
        GetQuotesHandlerRequest request, 
        CancellationToken cancellationToken);
}

public sealed class GetQuotesHandler(
    IReadonlyRepository<Quote> quoteRepository,
    IMapper mapper) : IGetQuotesHandler
{
    private readonly IReadonlyRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<GetQuotesHandlerResponse> HandleAsync(
        GetQuotesHandlerRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _quoteRepository.GetAsync(cancellationToken);

        var mappedResult = result.Select(_mapper.Map<Quote, QuoteResponseHandler>).ToList();

        var response = new GetQuotesHandlerResponse { Quotes = mappedResult };

        return response;
    }
}
