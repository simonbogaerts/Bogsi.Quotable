// <copyright file="IUnitOfWork.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>

namespace Bogsi.Quotable.Application.Interfaces.Utilities;

using System.Data;

/// <summary>
/// When the unit of work is finished, it will provide everything that needs to be done to change the database as a result of the work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Create and return a transaction.
    /// </summary>
    /// <returns>A new IDbTransaction object.</returns>
    IDbTransaction BeginTransaction();

    /// <summary>
    /// Persist all tracked changes to the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token used during async computing.</param>
    /// <returns>A boolean representing if the save was successful.</returns>
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
