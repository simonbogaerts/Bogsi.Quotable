using AutoMapper;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Handlers.Quotes;

public interface IUpdateQuoteHandler
{
    Task<UpdateQuoteHandlerResponse> HandleAsync(
        UpdateQuoteHandlerRequest request,
        CancellationToken cancellationToken);
}

public sealed record UpdateQuoteHandlerRequest
{
    public const int MinimumLength = 5;
    public const int MaximumLength = 1255;

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

    public async Task<UpdateQuoteHandlerResponse> HandleAsync(
        UpdateQuoteHandlerRequest request, 
        CancellationToken cancellationToken)
    {
        UpdateQuoteHandlerResponse response = new();

        var quote = _mapper.Map<UpdateQuoteHandlerRequest, Quote>(request);

        bool isSaveSuccess = false;

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            await _quoteRepository.UpdateAsync(quote, cancellationToken);

            isSaveSuccess = await _unitOfWork.SaveChangesAsync(cancellationToken);

            transaction.Commit();
        }
        catch (Exception)
        {
            // Log

            transaction.Rollback();
        }

        if (!isSaveSuccess)
        {
            return response;
        }

        return response;
    }
}
