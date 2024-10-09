// <copyright file="UpdateQuoteHandler.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>

namespace Bogsi.Quotable.Application.Handlers.Quotes;

using AutoMapper;

using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;

using CSharpFunctionalExtensions;

/// <summary>
/// The interface required for dependency injection.
/// </summary>
public interface IUpdateQuoteHandler
{
    /// <summary>
    /// Update an existing quote.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>An empty response object.</returns>
    Task<Result<UpdateQuoteHandlerResponse, QuotableError>> HandleAsync(
        UpdateQuoteHandlerRequest request,
        CancellationToken cancellationToken);
}

/// <summary>
/// Representation of the parameters that can be used in the Handler.
/// </summary>
public sealed record UpdateQuoteHandlerRequest
{
    /// <summary>
    /// Gets the public id.
    /// </summary>
    required public Guid PublicId { get; init; }

    /// <summary>
    /// Gets the Value.
    /// </summary>
    required public string Value { get; init; }
}

/// <summary>
/// Representation of the response of the Handler.
/// </summary>
public sealed record UpdateQuoteHandlerResponse
{

}

/// <summary>
/// This handler updates an existing quote.
/// </summary>
/// <param name="quoteRepository">A readonly repository for quote items.</param>
/// <param name="mapper">A configured instance of AutoMapper.</param>
/// <param name="unitOfWork">A unit of work to persist data and create migrations.</param>
public sealed class UpdateQuoteHandler(
    IRepository<Quote> quoteRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IUpdateQuoteHandler
{
    private readonly IRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    /// <inheritdoc/>
    public async Task<Result<UpdateQuoteHandlerResponse, QuotableError>> HandleAsync(
        UpdateQuoteHandlerRequest request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        var quote = _mapper.Map<UpdateQuoteHandlerRequest, Quote>(request);

        bool isSaveSuccess = false;

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var result = await _quoteRepository.UpdateAsync(quote, cancellationToken).ConfigureAwait(false);

            if (result.IsFailure)
            {
                return result.Error;
            }

            isSaveSuccess = await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }

        if (!isSaveSuccess)
        {
            return QuotableErrors.InternalError;
        }

        return new ();
    }
}
