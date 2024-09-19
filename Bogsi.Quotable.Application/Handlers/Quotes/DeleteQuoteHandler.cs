using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;
using CSharpFunctionalExtensions;

namespace Bogsi.Quotable.Application.Handlers.Quotes;

public interface IDeleteQuoteHandler
{
    Task<Result<DeleteQuoteHandlerResponse, QuotableError>> HandleAsync(
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

    public async Task<Result<DeleteQuoteHandlerResponse, QuotableError>> HandleAsync(
        DeleteQuoteHandlerRequest request, 
        CancellationToken cancellationToken)
    {
        var model = await _quoteRepository.GetByIdAsync(request.PublicId, cancellationToken);

        if (model.IsFailure) 
        {
            return model.Error;
        }

        bool isSaveSuccess = false; 

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            await _quoteRepository.DeleteAsync(model.Value, cancellationToken);

            isSaveSuccess = await _unitOfWork.SaveChangesAsync(cancellationToken);

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

        return new();
    }
}
