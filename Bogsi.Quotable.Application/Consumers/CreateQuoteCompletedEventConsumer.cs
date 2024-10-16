// -----------------------------------------------------------------------
// <copyright file="CreateQuoteCompletedEventConsumer.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Consumers;

using Bogsi.Quotable.Application.Events;

using MassTransit;

using Microsoft.Extensions.Logging;

/// <summary>
/// The consumer for CreateQuoteCompletedEvent.
/// </summary>
public class CreateQuoteCompletedEventConsumer : IConsumer<CreateQuoteCompletedEvent>
{
    private readonly ILogger<CreateQuoteCompletedEventConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateQuoteCompletedEventConsumer"/> class.
    /// </summary>
    /// <param name="logger">An instance of a Serilog logger.</param>
    public CreateQuoteCompletedEventConsumer(
        ILogger<CreateQuoteCompletedEventConsumer> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public Task Consume(ConsumeContext<CreateQuoteCompletedEvent> context)
    {
        ArgumentNullException.ThrowIfNull(context);

        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["Public-Id"] = context.Message.PublicId.ToString("D"),
        });

        _logger.LogInformation("Creation of Quote completed..");

        return Task.CompletedTask;
    }
}
