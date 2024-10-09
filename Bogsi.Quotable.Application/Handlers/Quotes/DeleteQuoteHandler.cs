// -----------------------------------------------------------------------
// <copyright file="DeleteQuoteHandler.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Handlers.Quotes;

using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;

using CSharpFunctionalExtensions;

/// <summary>
/// The interface required for dependency injection.
/// </summary>
public interface IDeleteQuoteHandler
{
    /// <summary>
    /// Delete an existing qoute.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>An empty response object.</returns>
    Task<Result<DeleteQuoteHandlerResponse, QuotableError>> HandleAsync(
        DeleteQuoteHandlerRequest request,
        CancellationToken cancellationToken);
}

/// <summary>
/// Representation of the parameters that can be used in the Handler.
/// </summary>
public sealed record DeleteQuoteHandlerRequest
{
    /// <summary>
    /// Gets the public id.
    /// </summary>
    public Guid PublicId { get; init; }
}

/// <summary>
/// Representation of the response of the Handler.
/// </summary>
public sealed record DeleteQuoteHandlerResponse
{

}

/// <summary>
/// This handler deletes an existing qoute.
/// </summary>
/// <param name="quoteRepository">A readonly repository for quote items.</param>
/// <param name="unitOfWork">A unit of work to persist data and create migrations.</param>
public sealed class DeleteQuoteHandler(
    IRepository<Quote> quoteRepository,
    IUnitOfWork unitOfWork) : IDeleteQuoteHandler
{
    private readonly IRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    /// <inheritdoc/>
    public async Task<Result<DeleteQuoteHandlerResponse, QuotableError>> HandleAsync(
        DeleteQuoteHandlerRequest request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        var model = await _quoteRepository.GetByIdAsync(request.PublicId, cancellationToken).ConfigureAwait(false);

        if (model.IsFailure)
        {
            return model.Error;
        }

        bool isSaveSuccess = false;

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            await _quoteRepository.DeleteAsync(model.Value, cancellationToken).ConfigureAwait(false);

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
