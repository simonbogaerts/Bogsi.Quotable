// -----------------------------------------------------------------------
// <copyright file="CachedQuoteRepository.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Infrastructure.Repositories;

using System.Text.Json;
using System.Threading;

using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Application.Utilities;

using CSharpFunctionalExtensions;

using Microsoft.Extensions.Caching.Distributed;

/// <summary>
/// Decorated version of the QuoteRepository to allow for caching.
/// </summary>
/// <param name="decorated">Implementation of the Repository for the Quote entity.</param>
/// <param name="cache">Distributed cache implementation.</param>
public sealed class CachedQuoteRepository(
    IRepository<Quote> decorated,
    IDistributedCache cache) : IRepository<Quote>
{
    private readonly IRepository<Quote> _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
    private readonly IDistributedCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));

    /// <inheritdoc/>
    public async Task<Result<CursorResponse<Quote>, QuotableError>> GetAsync(GetQuotesQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        string key = $"QUOTES:{request.Cursor}:{request.Size}:{request.Origin?.ToUpper()}:{request.Tag?.ToUpper()}:{request.SearchQuery?.ToUpper()}";

        var cachedQuotes = await _cache.GetStringAsync(key, cancellationToken).ConfigureAwait(false);

        if (string.IsNullOrWhiteSpace(cachedQuotes))
        {
            var quotes = await _decorated.GetAsync(request, cancellationToken).ConfigureAwait(false);

            if (quotes.IsSuccess)
            {
                await _cache
                    .SetStringAsync(
                        key,
                        JsonSerializer.Serialize(quotes.Value),
                        new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) },
                        cancellationToken)
                    .ConfigureAwait(false);
            }

            return quotes;
        }
        else
        {
            var quotes = JsonSerializer.Deserialize<CursorResponse<Quote>>(cachedQuotes);

            return quotes is not null
                ? quotes
                : QuotableErrors.CouldNotDeserialize;
        }
    }

    /// <inheritdoc/>
    public async Task<Result<Quote, QuotableError>> GetByIdAsync(Guid publicId, CancellationToken cancellationToken)
    {
        string key = $"QUOTE:{publicId}";

        var cachedQuote = await _cache.GetStringAsync(key, cancellationToken).ConfigureAwait(false);

        if (string.IsNullOrWhiteSpace(cachedQuote))
        {
            var quote = await _decorated.GetByIdAsync(publicId, cancellationToken).ConfigureAwait(false);

            if (quote.IsSuccess)
            {
                await _cache
                .SetStringAsync(
                    key,
                    JsonSerializer.Serialize(quote.Value),
                    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) },
                    cancellationToken)
                .ConfigureAwait(false);
            }

            return quote;
        }
        else
        {
            var quote = JsonSerializer.Deserialize<Quote>(cachedQuote);

            return quote is not null
                ? quote
                : QuotableErrors.CouldNotDeserialize;
        }
    }

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> CreateAsync(Quote model, CancellationToken cancellationToken)
        => await _decorated.CreateAsync(model, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> UpdateAsync(Quote model, CancellationToken cancellationToken)
    {
        if (model is null)
        {
            return QuotableErrors.InputRequired;
        }

        var result = await _decorated.UpdateAsync(model, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            return result;
        }

        await RemoveCacheByPublicId(model.PublicId, cancellationToken).ConfigureAwait(false);

        return Unit.Instance;
    }

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> DeleteAsync(Quote model, CancellationToken cancellationToken)
    {
        if (model is null)
        {
            return QuotableErrors.InputRequired;
        }

        var result = await _decorated.DeleteAsync(model, cancellationToken).ConfigureAwait(false);

        if (result.IsFailure)
        {
            return result;
        }

        await RemoveCacheByPublicId(model.PublicId, cancellationToken).ConfigureAwait(false);

        return Unit.Instance;
    }

    /// <inheritdoc/>
    public async Task<Result<bool, QuotableError>> ExistsAsync(Guid publicId, CancellationToken cancellationToken)
        => await _decorated.ExistsAsync(publicId, cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Remove a cache key by public id if it exists.
    /// </summary>
    /// <param name="publicId">public id of the quote.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Result object of Unit and QuotableError.</returns>
    private async Task RemoveCacheByPublicId(Guid publicId, CancellationToken cancellationToken)
    {
        string key = $"QUOTE:{publicId}";

        var cachedQuote = await _cache.GetStringAsync(key, cancellationToken).ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(cachedQuote))
        {
            await _cache.RemoveAsync(key, cancellationToken).ConfigureAwait(false);
        }
    }
}
