// <copyright file="IRepository.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>

namespace Bogsi.Quotable.Application.Interfaces.Repositories;

using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Models;

using CSharpFunctionalExtensions;

/// <summary>
/// Default repository abstraction for the methods that require change tracking.
/// </summary>
/// <typeparam name="T">Type the repository is for.</typeparam>
public interface IRepository<T> : IReadonlyRepository<T>
    where T : ModelBase
{
    /// <summary>
    /// Creates a new item of T.
    /// </summary>
    /// <param name="model">Representation of the new item.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Result object of Unit and QuotableError.</returns>
    Task<Result<Unit, QuotableError>> CreateAsync(T model, CancellationToken cancellationToken);

    /// <summary>
    /// Update an existing item of T.
    /// </summary>
    /// <param name="model">Representation of the updated item.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Result object of Unit and QuotableError.</returns>
    Task<Result<Unit, QuotableError>> UpdateAsync(T model, CancellationToken cancellationToken);

    /// <summary>
    /// Delete item of T.
    /// </summary>
    /// <param name="model">Representation of item to be deleted.</param>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>Result object of Unit and QuotableError.</returns>
    Task<Result<Unit, QuotableError>> DeleteAsync(T model, CancellationToken cancellationToken);
}
