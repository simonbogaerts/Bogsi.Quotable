﻿using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Abstract;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Handlers.Quotes;

public interface IGetQuotesHandler
{
    Task<GetQuotesHandlerResponse> HandleAsync(
        GetQuotesHandlerRequest request,
        CancellationToken cancellationToken);
}

public sealed record GetQuotesHandlerRequest
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public string? Origin { get; init; }
    public string? Tag { get; init; }
    public string? SearchQuery { get; init; }
    public string? OrderBy { get; init; }
    public string? Fields { get; init; }
}

public sealed record GetQuotesSingleQuoteHandlerResponse : AbstractQuoteResponse
{

}

public sealed class GetQuotesHandlerResponse : List<GetQuotesSingleQuoteHandlerResponse>
{
    
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

        var response = _mapper.Map<List<Quote>?, GetQuotesHandlerResponse>(result);

        return response;
    }
}