// <copyright file="CreateQuoteHandler.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>

namespace Bogsi.Quotable.Application.Handlers.Quotes;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Abstract;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;

using CSharpFunctionalExtensions;

/// <summary>
/// The interface required for dependency injection.
/// </summary>
public interface ICreateQuoteHandler
{
    /// <summary>
    /// Create a new quote based upon provided parameters.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>The created quote.</returns>
    Task<Result<CreateQuoteHandlerResponse, QuotableError>> HandleAsync(
        CreateQuoteHandlerRequest request,
        CancellationToken cancellationToken);
}

/// <summary>
/// Representation of the parameters that can be used in the Handler.
/// </summary>
public sealed record CreateQuoteHandlerRequest
{
    /// <summary>
    /// Gets the Value.
    /// </summary>
    required public string Value { get; init; }
}

/// <summary>
/// Representation of the response of the Handler.
/// </summary>
public sealed record CreateQuoteHandlerResponse : AbstractQuoteResponse
{
    /// <summary>
    /// Gets Created of the CreateQuoteHandlerResponse model.
    /// </summary>
    public DateTime Created { get; init; }

    /// <summary>
    /// Gets Updated of the CreateQuoteHandlerResponse model.
    /// </summary>
    public DateTime Updated { get; init; }
}

/// <summary>
/// This handler creates a new quote.
/// </summary>
/// <param name="quoteRepository">A readonly repository for quote items.</param>
/// <param name="mapper">A configured instance of AutoMapper.</param>
/// <param name="unitOfWork">A unit of work to persist data and create migrations.</param>
public sealed class CreateQuoteHandler(
    IRepository<Quote> quoteRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICreateQuoteHandler
{
    private readonly IRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    /// <inheritdoc/>
    public async Task<Result<CreateQuoteHandlerResponse, QuotableError>> HandleAsync(
        CreateQuoteHandlerRequest request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        Quote model = _mapper.Map<CreateQuoteHandlerRequest, Quote>(request);

        bool isSaveSuccess = false;

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var result = await _quoteRepository.CreateAsync(model, cancellationToken).ConfigureAwait(false);

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

        var response = _mapper.Map<Quote, CreateQuoteHandlerResponse>(model);

        return response;
    }
}
