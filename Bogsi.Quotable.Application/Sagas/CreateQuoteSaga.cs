// -----------------------------------------------------------------------
// <copyright file="CreateQuoteSaga.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Bogsi.Quotable.Application.Events;

using MassTransit;

namespace Bogsi.Quotable.Application.Sagas;

/// <summary>
/// The state machine for the CreateQuoteSaga.
/// </summary>
public sealed class CreateQuoteSaga : MassTransitStateMachine<CreateQuoteSagaData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateQuoteSaga"/> class.
    /// </summary>
    public CreateQuoteSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => CreateQuoteRequestedEvent, e => e.CorrelateById(m => m.Message.SagaId));
        Event(() => CreateQuoteCompletedEvent, e => e.CorrelateById(m => m.Message.SagaId));
        Event(() => CacheMaintenanceCompletedEvent, e => e.CorrelateById(m => m.Message.SagaId));
        Event(() => FinalizeFailedSagaEvent, e => e.CorrelateById(m => m.Message.SagaId));

        Initially(
            When(CreateQuoteRequestedEvent)
                .Then(context => context.Saga.PublicId = context.Message.PublicId)
                .TransitionTo(Creating)
                .Publish(context => new CreateQuoteEvent
                {
                    PublicId = context.Message.PublicId,
                    SagaId = context.Message.SagaId,
                    Model = context.Message.Model,
                }));

        During(
            Creating,
            When(CreateQuoteCompletedEvent)
                .Then(context => context.Saga.QuoteCreated = true)
                .TransitionTo(Maintenance)
                .Publish(context => new CacheMaintanenceRequestedEvent
                {
                    PublicId = context.Message.PublicId,
                    SagaId = context.Message.SagaId,
                    MaintenanceType = Enums.CacheMaintenanceType.AddOrUpdate,
                    Model = context.Message.Model,
                }));

        During(
            Maintenance,
            When(CacheMaintenanceCompletedEvent)
                .Then(context =>
                {
                    context.Saga.CacheCreated = true;
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
    /// Gets or sets the Creating state.
    /// </summary>
    required public State Creating { get; set; }

    /// <summary>
    /// Gets or sets the Auditing state.
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
    /// Gets or sets the CreateQuoteRequestedEvent.
    /// </summary>
    required public Event<CreateQuoteRequestedEvent> CreateQuoteRequestedEvent { get; set; }

    /// <summary>
    /// Gets or sets the CreateQuoteCompletedEvent.
    /// </summary>
    required public Event<CreateQuoteCompletedEvent> CreateQuoteCompletedEvent { get; set; }

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
/// Contains the data for the CreateQuoteSaga.
/// </summary>
public sealed class CreateQuoteSagaData : SagaStateMachineInstance
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
    /// Gets or sets a value indicating whether the quote has been created.
    /// </summary>
    public bool QuoteCreated { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the cache has been added.
    /// </summary>
    public bool CacheCreated { get; set; }

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
