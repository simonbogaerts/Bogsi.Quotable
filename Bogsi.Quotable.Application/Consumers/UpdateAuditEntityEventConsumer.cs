// -----------------------------------------------------------------------
// <copyright file="UpdateAuditEntityEventConsumer.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Consumers;

using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Enums;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Events;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;

using MassTransit;

using Microsoft.Extensions.Logging;

/// <summary>
/// The consumer for UpdateAuditEntityEvent.
/// Logic was originally done by UnitOfWork, but moved here to test out Sagas.
/// </summary>
public sealed class UpdateAuditEntityEventConsumer : IConsumer<UpdateAuditEntityEvent>
{
    private readonly IAuditableRepository<QuoteEntity> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateAuditEntityEventConsumer> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAuditEntityEventConsumer"/> class.
    /// </summary>
    /// <param name="repository">Implementation of the AuditableRepository for the Quote entity.</param>
    /// <param name="unitOfWork">A unit of work to persist data and create migrations.</param>
    /// <param name="logger">An instance of a Serilog logger.</param>
    public UpdateAuditEntityEventConsumer(
        IAuditableRepository<QuoteEntity> repository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateAuditEntityEventConsumer> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc/>
    public async Task Consume(ConsumeContext<UpdateAuditEntityEvent> context)
    {
        ArgumentNullException.ThrowIfNull(context);

        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["Public-Id"] = context.Message.PublicId.ToString("D"),
        });

        var message = context.Message;
        var cancellationToken = context.CancellationToken;

        _logger.LogInformation("Updating audit values for Quote..");

        using var transaction = _unitOfWork.BeginTransaction();

        bool isSaveSuccess = false;

        try
        {
            DateTime now = DateTime.UtcNow;

            var result = message.Type switch
            {
                AuditUpdateType.Create => await _repository.UpdateCreateAuditAsync(message.PublicId, now, now, cancellationToken).ConfigureAwait(false),
                AuditUpdateType.Update => await _repository.UpdateUpdateAuditAsync(message.PublicId, now, cancellationToken).ConfigureAwait(false),
                _ => QuotableErrors.NotFound,
            };

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
        }

        if (!isSaveSuccess)
        {
            _logger.LogError("Something went wrong while saving the transaction.");

            await context
                .Publish(
                    new FinalizeFailedEvent
                    {
                        PublicId = message.PublicId,
                    }, cancellationToken)
                .ConfigureAwait(false);
        }

        await context
            .Publish(
                new AuditEntityUpdatedEvent
                {
                    PublicId = message.PublicId,
                    IsSuccess = true,
                }, cancellationToken)
            .ConfigureAwait(false);
    }
}
