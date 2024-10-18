// -----------------------------------------------------------------------
// <copyright file="UpdateQuote.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Handlers.Quotes;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

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
public sealed record UpdateQuoteCommand
    : IRequest<Result<Unit, QuotableError>>
{
    /// <summary>
    /// Gets the public id.
    /// </summary>
    required public Guid PublicId { get; init; }

    /// <summary>
    /// Gets the Value.
    /// </summary>
    required public string Value { get; init; }
}

/// <summary>
/// This handler updates an existing quote.
/// </summary>
public sealed class UpdateQuoteHandler
    : IRequestHandler<UpdateQuoteCommand, Result<Unit, QuotableError>>
{
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateQuoteHandler"/> class.
    /// </summary>
    /// <param name="mapper">A configured instance of AutoMapper.</param>
    /// <param name="bus">A mass transit instance.</param>
    public UpdateQuoteHandler(
        IMapper mapper,
        IBus bus)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> Handle(
        UpdateQuoteCommand request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        var model = _mapper.Map<UpdateQuoteCommand, Quote>(request);

        await _bus
            .Publish(
                new UpdateQuoteRequestedEvent
                {
                    PublicId = model.PublicId,
                    Model = model,
                },
                cancellationToken)
            .ConfigureAwait(false);

        return Unit.Instance;
    }
}
