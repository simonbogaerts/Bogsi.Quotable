// -----------------------------------------------------------------------
// <copyright file="QuoteRepository.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Infrastructure.Repositories;

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

/// <summary>
/// Implementation of the Repository for the Quote entity.
/// </summary>
/// <param name="quotable">The database context.</param>
/// <param name="mapper">A configured instance of AutoMapper.</param>
public sealed class QuoteRepository(
    QuotableContext quotable,
    IMapper mapper) : IRepository<Quote>
{
    private readonly QuotableContext _quotable = quotable ?? throw new ArgumentNullException(nameof(quotable));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    /// <inheritdoc/>
    public async Task<Result<CursorResponse<Quote>, QuotableError>> GetAsync(
        GetQuotesHandlerRequest request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        var source = _quotable
            .Quotes
            .AsNoTracking()
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
        int total = await source.CountAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        var entities = await source
            .Where(x => x.Id >= request.Cursor)
            .OrderBy(x => x.Id)
            .Take(request.Size + Constants.Cursor.Offset)
            .ToListAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        int newCursor = entities.LastOrDefault()?.Id ?? Constants.Cursor.None;

        var selection = entities
            .Take(request.Size);

        bool hasNext = newCursor > selection.LastOrDefault()?.Id;

        var result = selection
            .Select(_mapper.Map<QuoteEntity, Quote>)
            .ToList();

        return new CursorResponse<Quote>()
        {
            Cursor = newCursor,
            Data = result,
            Size = request.Size,
            Total = total,
            HasNext = hasNext,
        };
    }

    /// <inheritdoc/>
    public async Task<Result<Quote, QuotableError>> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        var entity = await _quotable
            .Quotes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return QuotableErrors.NotFound;
        }

        var result = _mapper.Map<QuoteEntity, Quote>(entity);

        return result;
    }

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> CreateAsync(Quote model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Quote, QuoteEntity>(model);

        await _quotable.Quotes.AddAsync(entity, cancellationToken: cancellationToken).ConfigureAwait(false);

        return Unit.Instance;
    }

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> UpdateAsync(Quote model, CancellationToken cancellationToken)
    {
        var entity = await _quotable
            .Quotes
            .FirstOrDefaultAsync(x => x.PublicId == model.PublicId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return QuotableErrors.NotFound;
        }

        _mapper.Map(model, entity);

        _quotable.Quotes.Update(entity);

        return Unit.Instance;
    }

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> DeleteAsync(Quote model, CancellationToken cancellationToken)
    {
        var entity = await _quotable
            .Quotes
            .FirstOrDefaultAsync(x => x.PublicId == model.PublicId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return QuotableErrors.NotFound;
        }

        _quotable.Quotes.Remove(entity);

        return Unit.Instance;
    }

    /// <inheritdoc/>
    public async Task<Result<bool, QuotableError>> ExistsAsync(Guid publicId, CancellationToken cancellationToken)
    {
        var result = await _quotable
            .Quotes
            .AsNoTracking()
            .AnyAsync(x => x.PublicId == publicId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return result;
    }
}
