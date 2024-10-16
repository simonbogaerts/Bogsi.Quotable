// -----------------------------------------------------------------------
// <copyright file="AuditableQuoteRepository.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Persistence;

using CSharpFunctionalExtensions;

using Microsoft.EntityFrameworkCore;

namespace Bogsi.Quotable.Infrastructure.Repositories;

/// <summary>
/// Implementation of the Repository for the Quote entity.
/// </summary>
public sealed class AuditableQuoteRepository : IAuditableRepository<QuoteEntity>
{
    private readonly QuotableContext _quotable;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableQuoteRepository"/> class.
    /// </summary>
    /// <param name="quotable">The database context.</param>
    public AuditableQuoteRepository(
        QuotableContext quotable)
    {
        _quotable = quotable ?? throw new ArgumentNullException(nameof(quotable));
    }

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> UpdateCreateAuditAsync(
        Guid publicId,
        DateTime created,
        DateTime updated,
        CancellationToken cancellationToken)
    {
        QuoteEntity? entity = await _quotable
            .Quotes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return QuotableErrors.NotFound;
        }

        entity.Created = created;
        entity.Updated = updated;

        return Unit.Instance;
    }

    /// <inheritdoc/>
    public async Task<Result<Unit, QuotableError>> UpdateUpdateAuditAsync(
        Guid publicId,
        DateTime updated,
        CancellationToken cancellationToken)
    {
        QuoteEntity? entity = await _quotable
            .Quotes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return QuotableErrors.NotFound;
        }

        entity.Updated = updated;

        return Unit.Instance;
    }
}
