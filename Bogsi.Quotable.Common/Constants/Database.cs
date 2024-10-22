// -----------------------------------------------------------------------
// <copyright file="Database.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Constants;

/// <summary>
/// Different values concerning the database.
/// </summary>
public sealed record Database
{
    /// <summary>
    /// List of different schemas.
    /// </summary>
    public sealed record DatabaseNames
    {
        /// <summary>
        /// Name of the quotable database.
        /// </summary>
        public const string QuotableDb = nameof(QuotableDb);
    }

    /// <summary>
    /// List of different schemas.
    /// </summary>
    public sealed record Schemas
    {
        /// <summary>
        /// Name of the default schema.
        /// </summary>
        public const string Quotable = nameof(Quotable);

        /// <summary>
        /// Name of the saga schema.
        /// </summary>
        public const string Saga = nameof(Saga);
    }

    /// <summary>
    /// List of the tables.
    /// </summary>
    public sealed record Tables
    {
        /// <summary>
        /// Name of the table that has the Quote entities.
        /// </summary>
        public const string Quotes = nameof(Quotes);

        /// <summary>
        /// Name of the table that has the Quote entities.
        /// </summary>
        public const string CreateQuoteSagaData = nameof(CreateQuoteSagaData);

        /// <summary>
        /// Name of the table that has the Quote entities.
        /// </summary>
        public const string UpdateQuoteSagaData = nameof(UpdateQuoteSagaData);

        /// <summary>
        /// Name of the table that has the Quote entities.
        /// </summary>
        public const string DeleteQuoteSagaData = nameof(DeleteQuoteSagaData);
    }

    /// <summary>
    /// List of functions.
    /// </summary>
    public sealed record Functions
    {
        /// <summary>
        /// The GetDate method in PosgreSQL.
        /// </summary>
        public const string GetDate = "NOW()";
    }
}
