using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Abstract;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Application.Utilities;
using CSharpFunctionalExtensions;

namespace Bogsi.Quotable.Application.Handlers.Quotes;

public interface IGetQuotesHandler
{
    Task<Result<GetQuotesHandlerResponse, QuotableError>> HandleAsync(
        GetQuotesHandlerRequest request,
        CancellationToken cancellationToken);
}

public sealed record GetQuotesHandlerRequest
{
    public int Cursor { get; init; }
    public int Size { get; init; }
    public string? Origin { get; init; }
    public string? Tag { get; init; }
    public string? SearchQuery { get; init; }
}

public sealed record GetQuotesSingleQuoteHandlerResponse : AbstractQuoteResponse
{

}

public sealed record GetQuotesHandlerResponse : CursorResponse<List<GetQuotesSingleQuoteHandlerResponse>>
{

}

public sealed class GetQuotesHandler(
    IReadonlyRepository<Quote> quoteRepository,
    IMapper mapper) : IGetQuotesHandler
{
    private readonly IReadonlyRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<GetQuotesHandlerResponse, QuotableError>> HandleAsync(
        GetQuotesHandlerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _quoteRepository.GetAsync(request.PageNumber, request.PageSize, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error;
        }

        var response = _mapper.Map<CursorResponse<List<Quote>>, GetQuotesHandlerResponse>(result.Value);

        return response;
    }
}