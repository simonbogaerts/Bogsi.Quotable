// -----------------------------------------------------------------------
// <copyright file="DeleteQuote.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Handlers.Quotes;

using System.Threading;
using System.Threading.Tasks;

using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Events;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Application.Models;

using CSharpFunctionalExtensions;

using MassTransit;

using MediatR;

using Unit = Models.Unit;

/// <summary>
/// Representation of the parameters that can be used in the Handler.
/// </summary>
public sealed record DeleteQuoteCommand
    : IRequest<Result<Unit, QuotableError>>
{
    /// <summary>
    /// Gets the public id.
    /// </summary>
    public Guid PublicId { get; init; }
}

/// <summary>
/// This handler deletes an existing quote.
/// </summary>
public sealed class DeleteQuoteHandler
    : IRequestHandler<DeleteQuoteCommand, Result<Unit, QuotableError>>
{
    private readonly IRepository<Quote> _repository;
    private readonly IBus _bus;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteQuoteHandler"/> class.
    /// </summary>
    /// <param name="repository">A readonly repository for quote items.</param>
    /// <param name="bus">A mass transit instance.</param>
    public DeleteQuoteHandler(
        IRepository<Quote> repository,
        IBus bus)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> Handle(
        DeleteQuoteCommand request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        var model = await _repository.GetByIdAsync(request.PublicId, cancellationToken).ConfigureAwait(false);

        if (model.IsFailure)
        {
            return model.Error;
        }

        await _bus
            .Publish(
                new DeleteQuoteRequestedEvent
                {
                    PublicId = model.Value.PublicId,
                    Model = model.Value,
                },
                cancellationToken)
            .ConfigureAwait(false);

        return Unit.Instance;
    }
}
