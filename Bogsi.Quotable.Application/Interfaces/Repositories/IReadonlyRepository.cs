// -----------------------------------------------------------------------
// <copyright file="IReadonlyRepository.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Interfaces.Repositories;

using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Application.Utilities;

using CSharpFunctionalExtensions;

/// <summary>
/// Default repository abstraction for the methods that don't require change tracking.
/// </summary>
/// <typeparam name="T">Type the repository is for.</typeparam>
public interface IReadonlyRepository<T>
    where T : ModelBase
{
    /// <summary>
    /// Gets a collection matching the provided request parameters.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Result object of Cursor response of type T and QuotableError.</returns>
    Task<Result<CursorResponse<T>, QuotableError>> GetAsync(GetQuotesQuery request, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a single item matching the provided identifier.
    /// </summary>
    /// <param name="publicId">The public id of the item.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Result object of T and QuotableError.</returns>
    Task<Result<T, QuotableError>> GetByIdAsync(Guid publicId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a value indicating whether the item exists.
    /// </summary>
    /// <param name="publicId">The public id of the item.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Result object of bool and QuotableError.</returns>
    Task<Result<bool, QuotableError>> ExistsAsync(Guid publicId, CancellationToken cancellationToken);
}
