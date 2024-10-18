// -----------------------------------------------------------------------
// <copyright file="DeleteQuoteCompletedEventConsumer.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Consumers;

using Bogsi.Quotable.Application.Events;

using MassTransit;

using Microsoft.Extensions.Logging;

/// <summary>
/// The consumer for DeleteQuoteCompletedEvent.
/// </summary>
public sealed class DeleteQuoteCompletedEventConsumer : IConsumer<DeleteQuoteCompletedEvent>
{
    private readonly ILogger<DeleteQuoteCompletedEventConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteQuoteCompletedEventConsumer"/> class.
    /// </summary>
    /// <param name="logger">An instance of a Serilog logger.</param>
    public DeleteQuoteCompletedEventConsumer(
        ILogger<DeleteQuoteCompletedEventConsumer> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public Task Consume(ConsumeContext<DeleteQuoteCompletedEvent> context)
    {
        ArgumentNullException.ThrowIfNull(context);

        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["Public-Id"] = context.Message.PublicId.ToString("D"),
        });

        _logger.LogInformation("Deletion of Quote completed..");

        return Task.CompletedTask;
    }
}
