﻿// -----------------------------------------------------------------------
// <copyright file="Constants.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Persistence;

/// <summary>
/// Constants used in the Persistence Layer.
/// </summary>
internal sealed record Constants
{
    /// <summary>
    /// Name of the database.
    /// </summary>
    public const string QuotableDb = nameof(QuotableDb);

    /// <summary>
    /// List of different schemas.
    /// </summary>
    internal sealed record Schemas
    {
        /// <summary>
        /// Name of the default schema.
        /// </summary>
        public const string Quotable = nameof(Quotable);
    }

    /// <summary>
    /// List of the tables.
    /// </summary>
    internal sealed record Tables
    {
        /// <summary>
        /// Name of the table that has the Quote entities.
        /// </summary>
        public const string Quotes = nameof(Quotes);
    }

    /// <summary>
    /// List of functions.
    /// </summary>
    internal sealed record Functions
    {
        /// <summary>
        /// The GetDate method in PosgreSQL.
        /// </summary>
        public const string GetDate = "NOW()";
    }
}
