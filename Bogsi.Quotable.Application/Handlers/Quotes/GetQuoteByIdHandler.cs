using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Abstract;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Handlers.Quotes;

public interface IGetQuoteByIdHandler
{
    Task<GetQuoteByIdHandlerResponse?> HandleAsync(
        GetQuoteByIdHandlerRequest request,
        CancellationToken cancellationToken);
}

public sealed record GetQuoteByIdHandlerRequest
{
    public Guid PublicId { get; init; }
}

public sealed record GetQuoteByIdHandlerResponse : AbstractQuoteResponse
{
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}

public sealed class GetQuoteByIdHandler(
    IReadonlyRepository<Quote> quoteRepository,
    IMapper mapper) : IGetQuoteByIdHandler
{
    private readonly IReadonlyRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<GetQuoteByIdHandlerResponse?> HandleAsync(
        GetQuoteByIdHandlerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _quoteRepository.GetByIdAsync(request.PublicId, cancellationToken);

        if (result is null)
        {
            return null;
        }

        var response = _mapper.Map<Quote, GetQuoteByIdHandlerResponse>(result);

        return response;
    }
}