// -----------------------------------------------------------------------
// <copyright file="IAuditableRepository.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Interfaces.Repositories;

using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Models;

using CSharpFunctionalExtensions;

/// <summary>
/// Defaultt abstraction to update auditable entities.
/// </summary>
/// <typeparam name="T">Type the repository is for.</typeparam>
public interface IAuditableRepository<T>
    where T : EntityBase
{
    /// <summary>
    /// Update the Created and Updated fields of an AuditableEntity.
    /// </summary>
    /// <param name="publicId">Public id of the entity to update.</param>
    /// <param name="created">New Created date.</param>
    /// <param name="updated">New Updated date.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Discriminated union of Unit and QuotableError.</returns>
    Task<Result<Unit, QuotableError>> UpdateCreateAuditAsync(Guid publicId, DateTime created, DateTime updated, CancellationToken cancellationToken);

    /// <summary>
    /// Update the Updated field of an AuditableEntity.
    /// </summary>
    /// <param name="publicId">Public id of the entity to update.</param>
    /// <param name="updated">New Updated date.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Discriminated union of Unit and QuotableError.</returns>
    Task<Result<Unit, QuotableError>> UpdateUpdateAuditAsync(Guid publicId, DateTime updated, CancellationToken cancellationToken);
}
