// -----------------------------------------------------------------------
// <copyright file="UpdateQuoteEventConsumer.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Consumers.Quotes;

using System;

using Bogsi.Quotable.Application.Events;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;

using MassTransit;

using Microsoft.Extensions.Logging;

/// <summary>
/// The consumer for UpdateQuoteEvent.
/// </summary>
public sealed class UpdateQuoteEventConsumer : IConsumer<UpdateQuoteEvent>
{
    private readonly IRepository<Quote> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateQuoteEventConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateQuoteEventConsumer"/> class.
    /// </summary>
    /// <param name="repository">Implementation of the Repository for the Quote entity.</param>
    /// <param name="unitOfWork">A unit of work to persist data and create migrations.</param>
    /// <param name="logger">An instance of a Serilog logger.</param>
    public UpdateQuoteEventConsumer(
        IRepository<Quote> repository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateQuoteEventConsumer> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public async Task Consume(ConsumeContext<UpdateQuoteEvent> context)
    {
        ArgumentNullException.ThrowIfNull(context);

        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["Public-Id"] = context.Message.PublicId.ToString("D"),
        });

        var message = context.Message;
        var cancellationToken = context.CancellationToken;

        _logger.LogInformation("Updating Quote..");

        using var transaction = _unitOfWork.BeginTransaction();

        bool isSaveSuccess = false;

        try
        {
            var result = await _repository.UpdateAsync(message.Model, cancellationToken).ConfigureAwait(false);

            if (result.IsFailure)
            {
                _logger.LogError("{Code} - {Description}", result.Error.Code, result.Error.Description);
            }

            isSaveSuccess = await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            transaction.Commit();
        }
        catch (Exception exception)
        {
            _logger.LogError("Something went wrong: {Exception}", exception.Message);

            transaction.Rollback();

            isSaveSuccess = false;
        }

        if (!isSaveSuccess)
        {
            _logger.LogError("Something went wrong..");

            await context
                .Publish(
                    new FinalizeFailedSagaEvent
                    {
                        PublicId = message.PublicId,
                        SagaId = message.SagaId,
                    }, cancellationToken)
                .ConfigureAwait(false);
        }

        var response = await _repository.GetByIdAsync(message.PublicId, cancellationToken).ConfigureAwait(false);

        await context
            .Publish(
                new UpdateQuoteCompletedEvent
                {
                    PublicId = message.PublicId,
                    SagaId = message.SagaId,
                    Model = response.Value,
                }, cancellationToken)
            .ConfigureAwait(false);
    }
}
