// <copyright file="GetQuotesHandler.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>

namespace Bogsi.Quotable.Application.Handlers.Quotes;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Abstract;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Application.Utilities;

using CSharpFunctionalExtensions;

/// <summary>
/// The interface required for dependency injection.
/// </summary>
public interface IGetQuotesHandler
{
    /// <summary>
    /// Get all quotes matching the provided request.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Collection matching the request parameters.</returns>
    Task<Result<GetQuotesHandlerResponse, QuotableError>> HandleAsync(
        GetQuotesHandlerRequest request,
        CancellationToken cancellationToken);
}

/// <summary>
/// Representation of the parameters that can be used in the Handler.
/// </summary>
public sealed record GetQuotesHandlerRequest
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
public sealed record GetQuotesSingleQuoteHandlerResponse : AbstractQuoteResponse
{

}

/// <summary>
/// Representation of the response used in the Handler (collection).
/// </summary>
public sealed record GetQuotesHandlerResponse : CursorResponse<GetQuotesSingleQuoteHandlerResponse>
{

}

/// <summary>
/// This handler is fetches a collection of quotes matching the specified parameters.
/// </summary>
/// <param name="quoteRepository">A readonly repository for quote items.</param>
/// <param name="mapper">A configured instance of AutoMapper.</param>
public sealed class GetQuotesHandler(
    IReadonlyRepository<Quote> quoteRepository,
    IMapper mapper) : IGetQuotesHandler
{
    private readonly IReadonlyRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    /// <inheritdoc/>
    public async Task<Result<GetQuotesHandlerResponse, QuotableError>> HandleAsync(
        GetQuotesHandlerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _quoteRepository.GetAsync(request, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            return result.Error;
        }

        var response = _mapper.Map<CursorResponse<Quote>, GetQuotesHandlerResponse>(result.Value);

        return response;
    }
}
