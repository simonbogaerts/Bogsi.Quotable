// -----------------------------------------------------------------------
// <copyright file="GetQuoteByIdHandler.cs" company="BOGsi">
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

/// <summary>
/// The interface required for dependency injection.
/// </summary>
public interface IGetQuoteByIdHandler
{
    /// <summary>
    /// Get a quote by public id.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Single quote matching the public id.</returns>
    Task<Result<GetQuoteByIdHandlerResponse, QuotableError>> HandleAsync(
        GetQuoteByIdHandlerRequest request,
        CancellationToken cancellationToken);
}

/// <summary>
/// Representation of the parameters that can be used in the Handler.
/// </summary>
public sealed record GetQuoteByIdHandlerRequest
{
    /// <summary>
    /// Gets the public id.
    /// </summary>
    public Guid PublicId { get; init; }
}

/// <summary>
/// Representation of the response of the Handler.
/// </summary>
public sealed record GetQuoteByIdHandlerResponse : AbstractQuoteResponse
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
/// This handler fetches a single quote based upon its public id.
/// </summary>
/// <param name="quoteRepository">A readonly repository for quote items.</param>
/// <param name="mapper">A configured instance of AutoMapper.</param>
public sealed class GetQuoteByIdHandler(
    IReadonlyRepository<Quote> quoteRepository,
    IMapper mapper) : IGetQuoteByIdHandler
{
    private readonly IReadonlyRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    /// <inheritdoc/>
    public async Task<Result<GetQuoteByIdHandlerResponse, QuotableError>> HandleAsync(
        GetQuoteByIdHandlerRequest request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        var result = await _quoteRepository.GetByIdAsync(request.PublicId, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            return result.Error;
        }

        var response = _mapper.Map<Quote, GetQuoteByIdHandlerResponse>(result.Value);

        return response;
    }
}
