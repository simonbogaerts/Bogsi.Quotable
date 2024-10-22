// -----------------------------------------------------------------------
// <copyright file="CreateQuote.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Handlers.Quotes;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Events;
using Bogsi.Quotable.Application.Models;

using CSharpFunctionalExtensions;

using MassTransit;

using MediatR;

/// <summary>
/// Representation of the parameters that can be used in the Handler.
/// </summary>
public sealed record CreateQuoteCommand : IRequest<Result<Guid, QuotableError>>
{
    /// <summary>
    /// Gets the Value.
    /// </summary>
    required public string Value { get; init; }
}

/// <summary>
/// This handler creates a new quote.
/// </summary>
public sealed class CreateQuoteHandler
    : IRequestHandler<CreateQuoteCommand, Result<Guid, QuotableError>>
{
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateQuoteHandler"/> class.
    /// </summary>
    /// <param name="mapper">A configured instance of AutoMapper.</param>
    /// <param name="bus">A mass transit instance.</param>
    public CreateQuoteHandler(
        IMapper mapper,
        IBus bus)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    /// <inheritdoc/>
    public async Task<Result<Guid, QuotableError>> Handle(
        CreateQuoteCommand request,
        CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return QuotableErrors.InputRequired;
        }

        Quote model = _mapper.Map<CreateQuoteCommand, Quote>(request);

        await _bus
            .Publish(
                new CreateQuoteRequestedEvent
                {
                    PublicId = model.PublicId,
                    SagaId = Guid.NewGuid(),
                    Model = model,
                },
                cancellationToken)
            .ConfigureAwait(false);

        return model.PublicId;
    }
}
