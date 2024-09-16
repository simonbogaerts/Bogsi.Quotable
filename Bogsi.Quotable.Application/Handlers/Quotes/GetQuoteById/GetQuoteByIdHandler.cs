using AutoMapper;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Handlers.Quotes.GetQuoteById;

public interface IGetQuoteByIdHandler
{
    Task<GetQuoteByIdHandlerResponse> HandleAsync(
        GetQuoteByIdHandlerRequest request,
        CancellationToken cancellationToken);
}

public sealed class GetQuoteByIdHandler(
    IReadonlyRepository<Quote> quoteRepository,
    IMapper mapper) : IGetQuoteByIdHandler
{
    private readonly IReadonlyRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<GetQuoteByIdHandlerResponse> HandleAsync(
        GetQuoteByIdHandlerRequest request,
        CancellationToken cancellationToken)
    {
        var quote = await _quoteRepository.GetByIdAsync(request.PublicId, cancellationToken);

        var result = _mapper.Map<Quote, QuoteResponseHandler>(quote!);

        GetQuoteByIdHandlerResponse response = new()
        {
            Quote = result
        };

        return response;
    }
}
