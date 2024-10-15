// -----------------------------------------------------------------------
// <copyright file="DeleteQuote.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Handlers.Quotes;

using System.Threading;
using System.Threading.Tasks;

using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;

using CSharpFunctionalExtensions;

using MediatR;

using Unit = Models.Unit;

/// <summary>
/// Representation of the parameters that can be used in the Handler.
/// </summary>
public sealed record DeleteQuoteCommand
    : IRequest<Result<Unit, QuotableError>>
{
    /// <summary>
    /// Gets the public id.
    /// </summary>
    public Guid PublicId { get; init; }
}

/// <summary>
/// This handler deletes an existing quote.
/// </summary>
public sealed class DeleteQuoteHandler
    : IRequestHandler<DeleteQuoteCommand, Result<Unit, QuotableError>>
{
    private readonly IRepository<Quote> _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteQuoteHandler"/> class.
    /// </summary>
    /// <param name="repository">A readonly repository for quote items.</param>
    /// <param name="unitOfWork">A unit of work to persist data and create migrations.</param>
    public DeleteQuoteHandler(
        IRepository<Quote> repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> Handle(
        DeleteQuoteCommand request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        var model = await _repository.GetByIdAsync(request.PublicId, cancellationToken).ConfigureAwait(false);

        if (model.IsFailure)
        {
            return model.Error;
        }

        bool isSaveSuccess = false;

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            await _repository.DeleteAsync(model.Value, cancellationToken).ConfigureAwait(false);

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

        return Unit.Instance;
    }
}
