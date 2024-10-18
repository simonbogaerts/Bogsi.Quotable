// -----------------------------------------------------------------------
// <copyright file="CleanCacheEventConsumer.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Consumers;

using System.Threading.Tasks;

using Bogsi.Quotable.Application.Events;

using MassTransit;

using Microsoft.Extensions.Caching.Distributed;

/// <summary>
/// The consumer for CleanCacheEvent.
/// </summary>
public class CleanCacheEventConsumer : IConsumer<CleanCacheEvent>
{
    private readonly IDistributedCache _cache;

    /// <summary>
    /// Initializes a new instance of the <see cref="CleanCacheEventConsumer"/> class.
    /// </summary>
    /// <param name="cache">Distributed cache implementation.</param>
    public CleanCacheEventConsumer(
        IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    /// <inheritdoc/>
    public async Task Consume(ConsumeContext<CleanCacheEvent> context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var message = context.Message;
        var cancellationToken = context.CancellationToken;

        string key = $"QUOTE:{message.PublicId}";

        var cachedQuote = await _cache.GetStringAsync(key, cancellationToken).ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(cachedQuote))
        {
            await _cache.RemoveAsync(key, cancellationToken).ConfigureAwait(false);
        }

        await context
            .Publish(
                new CacheCleanedEvent
                {
                    PublicId = message.PublicId,
                }, cancellationToken)
            .ConfigureAwait(false);
    }
}
