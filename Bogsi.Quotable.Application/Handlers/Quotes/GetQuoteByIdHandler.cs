using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Abstract;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using CSharpFunctionalExtensions;

namespace Bogsi.Quotable.Application.Handlers.Quotes;

public interface IGetQuoteByIdHandler
{
    Task<Result<GetQuoteByIdHandlerResponse, QuotableError>> HandleAsync(
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

    public async Task<Result<GetQuoteByIdHandlerResponse, QuotableError>> HandleAsync(
        GetQuoteByIdHandlerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _quoteRepository.GetByIdAsync(request.PublicId, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error;
        }

        var response = _mapper.Map<Quote, GetQuoteByIdHandlerResponse>(result.Value);

        return response;
    }
}