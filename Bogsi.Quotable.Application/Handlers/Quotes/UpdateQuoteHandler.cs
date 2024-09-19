using AutoMapper;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;
using CSharpFunctionalExtensions;

namespace Bogsi.Quotable.Application.Handlers.Quotes;

public interface IUpdateQuoteHandler
{
    Task<Result<UpdateQuoteHandlerResponse, QuotableError>> HandleAsync(
        UpdateQuoteHandlerRequest request,
        CancellationToken cancellationToken);
}

public sealed record UpdateQuoteHandlerRequest
{
    public required Guid PublicId { get; init; }
    public required string Value { get; init; }
}

public sealed record UpdateQuoteHandlerResponse
{

}

public sealed class UpdateQuoteHandler(
    IRepository<Quote> quoteRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IUpdateQuoteHandler
{
    private readonly IRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Result<UpdateQuoteHandlerResponse, QuotableError>> HandleAsync(
        UpdateQuoteHandlerRequest request, 
        CancellationToken cancellationToken)
    {
        var quote = _mapper.Map<UpdateQuoteHandlerRequest, Quote>(request);

        bool isSaveSuccess = false;

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var result = await _quoteRepository.UpdateAsync(quote, cancellationToken);

            if (result.IsFailure) 
            {
                return result.Error;
            }

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
