using AutoMapper;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Handlers.Quotes.CreateQuote;

public interface ICreateQuoteHandler
{
    Task<CreateQuoteHandlerResponse> HandleAsync(
        CreateQuoteHandlerRequest request,
        CancellationToken cancellationToken);
}

public sealed class CreateQuoteHandler(
    IRepository<Quote> quoteRepository, 
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICreateQuoteHandler
{
    private readonly IRepository<Quote> _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<CreateQuoteHandlerResponse> HandleAsync(
        CreateQuoteHandlerRequest request, 
        CancellationToken cancellationToken)
    {
        Quote model = _mapper.Map<CreateQuoteHandlerRequest, Quote>(request);

        await _quoteRepository.CreateAsync(model, cancellationToken);

        bool isSaveSuccess = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (!isSaveSuccess)
        {
            // when impl result pattern return error here.
        }

        var result = _mapper.Map<Quote, CreateQuoteHandlerResponse>(model);

        return result;
    }
}
