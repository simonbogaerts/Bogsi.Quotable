// -----------------------------------------------------------------------
// <copyright file="CacheMaintanenceRequestedEventConsumer.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Consumers.Quotes;

using System.Text.Json;

using Bogsi.Quotable.Application.Events;

using MassTransit;

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

/// <summary>
/// The consumer for CacheMaintanenceRequestedEvent.
/// </summary>
public sealed class CacheMaintanenceRequestedEventConsumer : IConsumer<CacheMaintanenceRequestedEvent>
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheMaintanenceRequestedEventConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheMaintanenceRequestedEventConsumer"/> class.
    /// </summary>
    /// <param name="cache">Distributed cache implementation.</param>
    /// <param name="logger">An instance of a Serilog logger.</param>
    public CacheMaintanenceRequestedEventConsumer(
        IDistributedCache cache,
        ILogger<CacheMaintanenceRequestedEventConsumer> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public async Task Consume(ConsumeContext<CacheMaintanenceRequestedEvent> context)
    {
        ArgumentNullException.ThrowIfNull(context);

        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["Public-Id"] = context.Message.PublicId.ToString("D"),
        });

        var message = context.Message;
        var cancellationToken = context.CancellationToken;

        string key = $"QUOTE:{message.PublicId}";

        await _cache.RemoveAsync(key, cancellationToken).ConfigureAwait(false);

        if (message.MaintenanceType is Enums.CacheMaintenanceType.AddOrUpdate && message.Model is not null)
        {
            _logger.LogWarning("Maintenance is {Type} but model is null.", message.MaintenanceType);

            await _cache
                .SetStringAsync(
                    key,
                    JsonSerializer.Serialize(message.Model),
                    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) },
                    cancellationToken)
                .ConfigureAwait(false);
        }

        await context
            .Publish(
                new CacheMaintenanceCompletedEvent
                {
                    PublicId = message.PublicId,
                    SagaId = message.SagaId,
                }, cancellationToken)
            .ConfigureAwait(false);
    }
}
