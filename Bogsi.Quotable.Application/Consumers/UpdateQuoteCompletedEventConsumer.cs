// -----------------------------------------------------------------------
// <copyright file="UpdateQuoteCompletedEventConsumer.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Consumers;

using Bogsi.Quotable.Application.Events;

using MassTransit;

using Microsoft.Extensions.Logging;

/// <summary>
/// The consumer for UpdateQuoteCompletedEvent.
/// </summary>
public class UpdateQuoteCompletedEventConsumer : IConsumer<UpdateQuoteCompletedEvent>
{
    private readonly ILogger<UpdateQuoteCompletedEventConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateQuoteCompletedEventConsumer"/> class.
    /// </summary>
    /// <param name="logger">An instance of a Serilog logger.</param>
    public UpdateQuoteCompletedEventConsumer(
        ILogger<UpdateQuoteCompletedEventConsumer> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public Task Consume(ConsumeContext<UpdateQuoteCompletedEvent> context)
    {
        ArgumentNullException.ThrowIfNull(context);

        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["Public-Id"] = context.Message.PublicId.ToString("D"),
        });

        _logger.LogInformation("Update of Quote completed..");

        return Task.CompletedTask;
    }
}
