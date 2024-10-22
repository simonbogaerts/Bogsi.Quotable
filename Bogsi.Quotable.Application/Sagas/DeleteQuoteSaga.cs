// -----------------------------------------------------------------------
// <copyright file="DeleteQuoteSaga.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Sagas;

using Bogsi.Quotable.Application.Events;

using MassTransit;

/// <summary>
/// The state machine for the DeleteQuoteSaga.
/// </summary>
public sealed class DeleteQuoteSaga : MassTransitStateMachine<DeleteQuoteSagaData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteQuoteSaga"/> class.
    /// </summary>
    public DeleteQuoteSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => DeleteQuoteRequestedEvent, e => e.CorrelateById(m => m.Message.SagaId));
        Event(() => DeleteQuoteCompletedEvent, e => e.CorrelateById(m => m.Message.SagaId));
        Event(() => CacheMaintenanceCompletedEvent, e => e.CorrelateById(m => m.Message.SagaId));
        Event(() => FinalizeFailedSagaEvent, e => e.CorrelateById(m => m.Message.SagaId));

        Initially(
            When(DeleteQuoteRequestedEvent)
                .Then(context => context.Saga.PublicId = context.Message.PublicId)
                .TransitionTo(Deleting)
                .Publish(context => new DeleteQuoteEvent
                {
                    PublicId = context.Message.PublicId,
                    SagaId = context.Message.SagaId,
                    Model = context.Message.Model,
                }));

        During(
            Deleting,
            When(DeleteQuoteCompletedEvent)
                .Then(context => context.Saga.QuoteDeleted = true)
                .TransitionTo(Maintenance)
                .Publish(context => new CacheMaintanenceRequestedEvent
                {
                    PublicId = context.Message.PublicId,
                    SagaId = context.Message.SagaId,
                    MaintenanceType = Enums.CacheMaintenanceType.Delete,
                }));

        During(
            Maintenance,
            When(CacheMaintenanceCompletedEvent)
                .Then(context =>
                {
                    context.Saga.CacheCleaned = true;
                    context.Saga.SagaFinalized = true;
                })
                .TransitionTo(Finalizing)
                .Publish(context => new QuoteSagaCompletedEvent
                {
                    PublicId = context.Message.PublicId,
                    SagaId = context.Message.SagaId,
                })
            .Finalize());

        DuringAny(
            When(FinalizeFailedSagaEvent)
                .Then(context => context.Saga.SagaFailed = true)
                .TransitionTo(Failed)
            .Finalize());
    }

    #region State

    /// <summary>
    /// Gets or sets the Deleting state.
    /// </summary>
    required public State Deleting { get; set; }

    /// <summary>
    /// Gets or sets the Maintenance state.
    /// </summary>
    required public State Maintenance { get; set; }

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
    /// Gets or sets the DeleteQuoteRequestedEvent.
    /// </summary>
    required public Event<DeleteQuoteRequestedEvent> DeleteQuoteRequestedEvent { get; set; }

    /// <summary>
    /// Gets or sets the DeleteQuoteCompletedEvent.
    /// </summary>
    required public Event<DeleteQuoteCompletedEvent> DeleteQuoteCompletedEvent { get; set; }

    /// <summary>
    /// Gets or sets the CacheMaintenanceCompletedEvent.
    /// </summary>
    required public Event<CacheMaintenanceCompletedEvent> CacheMaintenanceCompletedEvent { get; set; }

    /// <summary>
    /// Gets or sets the FinalizeFailedSagaEvent.
    /// </summary>
    required public Event<FinalizeFailedSagaEvent> FinalizeFailedSagaEvent { get; set; }

    #endregion
}

/// <summary>
/// Contains the data for the DeleteQuoteSaga.
/// </summary>
public sealed class DeleteQuoteSagaData : SagaStateMachineInstance
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
    /// Gets or sets a value indicating whether the quote has been deleted.
    /// </summary>
    public bool QuoteDeleted { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the cache regarding the quote has been cleaned.
    /// </summary>
    public bool CacheCleaned { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the saga has been sucesfully tun to the end.
    /// </summary>
    public bool SagaFinalized { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the saga has failed.
    /// </summary>
    public bool SagaFailed { get; set; }

    /// <summary>
    /// Gets or sets the row version, required for optimistic concurrency.
    /// </summary>
    required public uint RowVersion { get; set; }
}
