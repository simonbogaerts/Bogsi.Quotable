// -----------------------------------------------------------------------
// <copyright file="UpdateQuote.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Handlers.Quotes;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

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
public sealed record UpdateQuoteCommand
    : IRequest<Result<Unit, QuotableError>>
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
/// This handler updates an existing quote.
/// </summary>
public sealed class UpdateQuoteHandler
    : IRequestHandler<UpdateQuoteCommand, Result<Unit, QuotableError>>
{
    private readonly IRepository<Quote> _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateQuoteHandler"/> class.
    /// </summary>
    /// <param name="repository">A readonly repository for quote items.</param>
    /// <param name="mapper">A configured instance of AutoMapper.</param>
    /// <param name="unitOfWork">A unit of work to persist data and create migrations.</param>
    public UpdateQuoteHandler(
        IRepository<Quote> repository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> Handle(
        UpdateQuoteCommand request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        var quote = _mapper.Map<UpdateQuoteCommand, Quote>(request);

        bool isSaveSuccess = false;

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var result = await _repository.UpdateAsync(quote, cancellationToken).ConfigureAwait(false);

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

        return Unit.Instance;
    }
}
