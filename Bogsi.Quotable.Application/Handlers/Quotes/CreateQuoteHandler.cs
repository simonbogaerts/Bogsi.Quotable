using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Abstract;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;
using CSharpFunctionalExtensions;

namespace Bogsi.Quotable.Application.Handlers.Quotes;

public interface ICreateQuoteHandler
{
    Task<Result<CreateQuoteHandlerResponse, QuotableError>> HandleAsync(
        CreateQuoteHandlerRequest request,
        CancellationToken cancellationToken);
}

public sealed record CreateQuoteHandlerRequest
{
    public required string Value { get; init; }
}

public sealed record CreateQuoteHandlerResponse : AbstractQuoteResponse
{
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}

public sealed class CreateQuoteHandler(
    IRepository<Quote> quoteRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICreateQuoteHandler
{
    private readonly IRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Result<CreateQuoteHandlerResponse, QuotableError>> HandleAsync(
        CreateQuoteHandlerRequest request,
        CancellationToken cancellationToken)
    {
        Quote model = _mapper.Map<CreateQuoteHandlerRequest, Quote>(request);

        bool isSaveSuccess = false;

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var result = await _quoteRepository.CreateAsync(model, cancellationToken);

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

        var response = _mapper.Map<Quote, CreateQuoteHandlerResponse>(model);

        return response;
    }
}
