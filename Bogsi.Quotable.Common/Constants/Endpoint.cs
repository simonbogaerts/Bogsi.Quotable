// -----------------------------------------------------------------------
// <copyright file="Endpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Constants;

/// <summary>
/// Extensions regarding endpoints.
/// </summary>
public sealed record Endpoint
{
    /// <summary>
    /// The different endpoint groups.
    /// </summary>
    public sealed record EndpointGroups
    {
        /// <summary>
        /// Name of the quotes endpoint group.
        /// </summary>
        public const string Quotes = nameof(Quotes);

        /// <summary>
        /// Name of the utilities endpoint group.
        /// </summary>
        public const string Utilities = nameof(Utilities);
    }

    /// <summary>
    /// Names of the specific endpoints regarding quotes.
    /// </summary>
    public sealed record QuoteEndpoints
    {
        /// <summary>
        /// Name of the GetQuotesEndpoint.
        /// </summary>
        public const string GetQuotesEndpoint = nameof(GetQuotesEndpoint);

        /// <summary>
        /// Name of the GetQuoteByIdEndpoint.
        /// </summary>
        public const string GetQuoteByIdEndpoint = nameof(GetQuoteByIdEndpoint);

        /// <summary>
        /// Name of the CreateQuoteEndpoint.
        /// </summary>
        public const string CreateQuoteEndpoint = nameof(CreateQuoteEndpoint);

        /// <summary>
        /// Name of the UpdateQuoteEndpoint.
        /// </summary>
        public const string UpdateQuoteEndpoint = nameof(UpdateQuoteEndpoint);

        /// <summary>
        /// Name of the DeleteQuoteEndpoint.
        /// </summary>
        public const string DeleteQuoteEndpoint = nameof(DeleteQuoteEndpoint);
    }
}
