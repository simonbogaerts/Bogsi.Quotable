// -----------------------------------------------------------------------
// <copyright file="UpdateQuoteSaga.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Sagas;

using Bogsi.Quotable.Application.Events;

using MassTransit;

/// <summary>
/// The state machine for the UpdateQuoteSaga.
/// </summary>
public sealed class UpdateQuoteSaga : MassTransitStateMachine<UpdateQuoteSagaData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateQuoteSaga"/> class.
    /// </summary>
    public UpdateQuoteSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => UpdateQuoteRequestedEvent, e => e.CorrelateById(m => m.Message.PublicId));
        Event(() => QuoteUpdatedEvent, e => e.CorrelateById(m => m.Message.PublicId));
        Event(() => AuditEntityUpdatedEvent, e => e.CorrelateById(m => m.Message.PublicId));
        Event(() => CacheCleanedEvent, e => e.CorrelateById(m => m.Message.PublicId));
        Event(() => FinalizeFailedEvent, e => e.CorrelateById(m => m.Message.PublicId));

        Initially(
            When(UpdateQuoteRequestedEvent)
                .Then(context => context.Saga.PublicId = context.Message.PublicId)
                .TransitionTo(Updating)
                .Publish(context => new UpdateQuoteEvent
                {
                    PublicId = context.Message.PublicId,
                    Model = context.Message.Model,
                }));

        During(
            Updating,
            When(QuoteUpdatedEvent)
                .Then(context => context.Saga.QuoteUpdated = true)
                .TransitionTo(Auditing)
                .Publish(context => new UpdateAuditEntityEvent
                {
                    PublicId = context.Message.PublicId,
                    Type = Enums.AuditUpdateType.Update,
                }));

        During(
            Auditing,
            When(AuditEntityUpdatedEvent)
                .Then(context => context.Saga.EntityAudited = true)
                .TransitionTo(Cleaning)
                .Publish(context => new CleanCacheEvent
                {
                    PublicId = context.Message.PublicId,
                }));

        During(
            Cleaning,
            When(CacheCleanedEvent)
                .Then(context =>
                {
                    context.Saga.CacheCleaned = true;
                    context.Saga.SagaFinalized = true;
                })
                .TransitionTo(Finalizing)
                .Publish(context => new UpdateQuoteCompletedEvent
                {
                    PublicId = context.Message.PublicId,
                })
            .Finalize());

        DuringAny(
            When(FinalizeFailedEvent)
                .TransitionTo(Failed)
            .Finalize());
    }

    #region State

    /// <summary>
    /// Gets or sets the Updating state.
    /// </summary>
    required public State Updating { get; set; }

    /// <summary>
    /// Gets or sets the Auditing state.
    /// </summary>
    required public State Auditing { get; set; }

    /// <summary>
    /// Gets or sets the Cleaning state.
    /// </summary>
    required public State Cleaning { get; set; }

    /// <summary>
    /// Gets or sets the Finalized state.
    /// </summary>
    required public State Finalizing { get; set; }

    /// <summary>
    /// Gets or sets the Failed state.
    /// </summary>
    required public State Failed { get; set; }

    #endregion

    #region Events

    /// <summary>
    /// Gets or sets the UpdateQuoteRequestedEvent.
    /// </summary>
    required public Event<UpdateQuoteRequestedEvent> UpdateQuoteRequestedEvent { get; set; }

    /// <summary>
    /// Gets or sets the QuoteUpdatedEvent.
    /// </summary>
    required public Event<QuoteUpdatedEvent> QuoteUpdatedEvent { get; set; }

    /// <summary>
    /// Gets or sets the AuditEntityUpdatedEvent.
    /// </summary>
    required public Event<AuditEntityUpdatedEvent> AuditEntityUpdatedEvent { get; set; }

    /// <summary>
    /// Gets or sets the CacheCleanedEvent.
    /// </summary>
    required public Event<CacheCleanedEvent> CacheCleanedEvent { get; set; }

    /// <summary>
    /// Gets or sets the FinalizeFailedEvent.
    /// </summary>
    required public Event<FinalizeFailedEvent> FinalizeFailedEvent { get; set; }

    #endregion
}

/// <summary>
/// Contains the data for the UpdateQuoteSaga.
/// </summary>
public sealed class UpdateQuoteSagaData : SagaStateMachineInstance
{
    /// <summary>
    /// Gets or sets the correlation id.
    /// </summary>
    required public Guid CorrelationId { get; set; }

    /// <summary>
    /// Gets or sets the current state..
    /// </summary>
    required public string CurrentState { get; set; }

    /// <summary>
    /// Gets or sets the public id.
    /// </summary>
    required public Guid PublicId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the quote has been updated.
    /// </summary>
    public bool QuoteUpdated { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the auditable dates have been updated.
    /// </summary>
    public bool EntityAudited { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the cache regarding the quote has been cleaned.
    /// </summary>
    public bool CacheCleaned { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the saga has been sucesfully tun to the end.
    /// </summary>
    public bool SagaFinalized { get; set; }
}
