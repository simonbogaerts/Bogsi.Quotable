using AutoMapper;

using Bogsi.Quotable.Application;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Application.Utilities;
using Bogsi.Quotable.Persistence;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Bogsi.Quotable.Infrastructure.Repositories;

public sealed class QuoteRepository(
    QuotableContext quotable,
    IMapper mapper) : IRepository<Quote>
{
    private readonly QuotableContext _quotable = quotable ?? throw new ArgumentNullException(nameof(quotable));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<CursorResponse<List<Quote>>, QuotableError>> GetAsync(
        GetQuotesHandlerRequest request, 
        CancellationToken cancellationToken)
    {
        var source = _quotable
            .Quotes
            .AsQueryable();

        // filtering
        if (request.Origin is not null)
        {

        }

        if (request.Tag is not null)
        {

        }

        // searching 
        if (request.SearchQuery is not null)
        {
            var searchQueryWhereClause = request.SearchQuery.Trim().ToUpperInvariant();

            source = source.Where(x => x.Value.ToUpper().Contains(searchQueryWhereClause));
        }

        // pagination
        var entities = await source
            .Where(x => x.Id >= request.Cursor)
            .Take(request.Size + Constants.Cursor.Offset)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken: cancellationToken);

        int newCursor = entities.Last().Id;

        var result = entities
            .Select(_mapper.Map<QuoteEntity, Quote>)
            .ToList();

        return new CursorResponse<List<Quote>>()
        {
            Cursor = newCursor,
            Data = result,
            Size = request.Size,
            Total = source.Count()
        };
    }

    public async Task<Result<Quote, QuotableError>> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        var entity = await _quotable
            .Quotes
            .FirstOrDefaultAsync(x=> x.PublicId == publicId, cancellationToken: cancellationToken);

        if (entity is null) 
        {
            return QuotableErrors.NotFound;
        }

        var result = _mapper.Map<QuoteEntity, Quote>(entity);

        return result;
    }

    public async Task<Result<Unit, QuotableError>> CreateAsync(Quote model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Quote, QuoteEntity>(model);

        await _quotable.Quotes.AddAsync(entity, cancellationToken: cancellationToken);

        return Unit.Instance;
    }

    public async Task<Result<Unit, QuotableError>> UpdateAsync(Quote model, CancellationToken cancellationToken)
    {
        var entity = await _quotable
            .Quotes
            .FirstOrDefaultAsync(x => x.PublicId == model.PublicId, cancellationToken: cancellationToken);

        if (entity is null)
        {
            return QuotableErrors.NotFound;
        }

        _mapper.Map(model, entity);

        _quotable.Quotes.Update(entity);

        return Unit.Instance;
    }

    public async Task<Result<Unit, QuotableError>> DeleteAsync(Quote model, CancellationToken cancellationToken)
    {
        var entity = await _quotable
            .Quotes
            .FirstOrDefaultAsync(x => x.PublicId == model.PublicId, cancellationToken: cancellationToken);

        if (entity is null)
        {
            return QuotableErrors.NotFound;
        }

        _quotable.Quotes.Remove(entity);

        return Unit.Instance;
    }

    public async Task<Result<bool, QuotableError>> ExistsAsync(Guid publicId, CancellationToken cancellationToken)
    {
        var result = await _quotable
            .Quotes
            .AnyAsync(x => x.PublicId == publicId, cancellationToken: cancellationToken);

        return result;
    }
}
