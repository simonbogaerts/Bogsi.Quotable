// -----------------------------------------------------------------------
// <copyright file="GetQuotes.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Handlers.Quotes;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Abstract;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Application.Utilities;

using CSharpFunctionalExtensions;

using MediatR;

/// <summary>
/// Representation of the parameters that can be used in the Handler.
/// </summary>
public sealed record GetQuotesQuery : IRequest<Result<GetQuotesResponse, QuotableError>>
{
    /// <summary>
    /// Gets the Cursor of the GetQuotesHandlerRequest model.
    /// </summary>
    public int Cursor { get; init; }

    /// <summary>
    /// Gets the Size of the GetQuotesHandlerRequest model.
    /// </summary>
    public int Size { get; init; }

    /// <summary>
    /// Gets the Origin of the GetQuotesHandlerRequest model.
    /// </summary>
    public string? Origin { get; init; }

    /// <summary>
    /// Gets the Tag of the GetQuotesHandlerRequest model.
    /// </summary>
    public string? Tag { get; init; }

    /// <summary>
    /// Gets the SearchQuery of the GetQuotesHandlerRequest model.
    /// </summary>
    public string? SearchQuery { get; init; }
}

/// <summary>
/// Representation of the response used in the Handler (single item).
/// </summary>
public sealed record GetQuotesSingleQuoteResponse : AbstractQuoteResponse
{

}

/// <summary>
/// Representation of the response used in the Handler (collection).
/// </summary>
public sealed record GetQuotesResponse : CursorResponse<GetQuotesSingleQuoteResponse>
{

}

/// <summary>
/// This handler gets all quotes matching the query.
/// </summary>
public sealed class GetQuotesHandler
    : IRequestHandler<GetQuotesQuery, Result<GetQuotesResponse, QuotableError>>
{
    private readonly IReadonlyRepository<Quote> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetQuotesHandler"/> class.
    /// </summary>
    /// <param name="repository">A readonly repository for quote items.</param>
    /// <param name="mapper">A configured instance of AutoMapper.</param>
    public GetQuotesHandler(
        IReadonlyRepository<Quote> repository,
        IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc/>
    public async Task<Result<GetQuotesResponse, QuotableError>> Handle(
        GetQuotesQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _repository.GetAsync(request, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            return result.Error;
        }

        var response = _mapper.Map<CursorResponse<Quote>, GetQuotesResponse>(result.Value);

        return response;
    }
}
