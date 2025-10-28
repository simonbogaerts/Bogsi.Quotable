// -----------------------------------------------------------------------
// <copyright file="GetQuoteById.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Handlers.Quotes;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Abstract;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;

using CSharpFunctionalExtensions;

using MediatR;

/// <summary>
/// Representation of the parameters that can be used in the Handler.
/// </summary>
public sealed record GetQuoteByIdQuery : IRequest<Result<GetQuoteByIdResponse, QuotableError>>
{
    /// <summary>
    /// Gets the public id.
    /// </summary>
    public Guid PublicId { get; init; }
}

/// <summary>
/// Representation of the response of the Handler.
/// </summary>
public sealed record GetQuoteByIdResponse : AbstractQuoteResponse
{
    /// <summary>
    /// Gets Created of the GetQuoteByIdHandlerResponse model.
    /// </summary>
    public DateTime Created { get; init; }

    /// <summary>
    /// Gets Updated of the GetQuoteByIdHandlerResponse model.
    /// </summary>
    public DateTime Updated { get; init; }
}

/// <summary>
/// This handler gets an existing quote by public id.
/// </summary>
public sealed class GetQuoteByIdHandler
    : IRequestHandler<GetQuoteByIdQuery, Result<GetQuoteByIdResponse, QuotableError>>
{
    private readonly IReadonlyRepository<Quote> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetQuoteByIdHandler"/> class.
    /// </summary>
    /// <param name="repository">A readonly repository for quote items.</param>
    /// <param name="mapper">A configured instance of AutoMapper.</param>
    public GetQuoteByIdHandler(
        IReadonlyRepository<Quote> repository,
        IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc/>
    public async Task<Result<GetQuoteByIdResponse, QuotableError>> Handle(
        GetQuoteByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        var result = await _repository.GetByIdAsync(request.PublicId, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            return result.Error;
        }

        var response = _mapper.Map<Quote, GetQuoteByIdResponse>(result.Value);

        return response;
    }
}
