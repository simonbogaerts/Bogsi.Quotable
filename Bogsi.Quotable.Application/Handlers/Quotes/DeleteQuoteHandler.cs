﻿using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Handlers.Quotes;

public interface IDeleteQuoteHandler
{
    Task<DeleteQuoteHandlerResponse> HandleAsync(
        DeleteQuoteHandlerRequest request,
        CancellationToken cancellationToken);
}

public sealed record DeleteQuoteHandlerRequest
{
    public Guid PublicId { get; init; }
}

public sealed record DeleteQuoteHandlerResponse
{

}

public sealed class DeleteQuoteHandler(
    IRepository<Quote> quoteRepository,
    IUnitOfWork unitOfWork) : IDeleteQuoteHandler
{
    private readonly IRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<DeleteQuoteHandlerResponse> HandleAsync(
        DeleteQuoteHandlerRequest request, 
        CancellationToken cancellationToken)
    {
        DeleteQuoteHandlerResponse response = new();

        var model = await _quoteRepository.GetByIdAsync(request.PublicId, cancellationToken);

        if (model is not null)
        {
            await _quoteRepository.DeleteAsync(model, cancellationToken);

            var isSaveSuccess = await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return response;
    }
}
