// -----------------------------------------------------------------------
// <copyright file="Constants.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web;

/// <summary>
/// Constants used in the Web Layer.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Name of the keycloak section.
    /// </summary>
    public const string Keycloak = nameof(Keycloak);

    /// <summary>
    /// Name of the authentication schema.
    /// </summary>
    public const string Bearer = nameof(Bearer);

    /// <summary>
    /// Different environments.
    /// </summary>
    public static class Environments
    {
        /// <summary>
        /// Production environment.
        /// </summary>
        public const string Production = nameof(Production);

        /// <summary>
        /// Test environment.
        /// </summary>
        public const string Testing = nameof(Testing);

        /// <summary>
        /// Test environment.
        /// </summary>
        public const string Development = nameof(Development);
    }

    /// <summary>
    /// Different keys for all connectionstrings.
    /// </summary>
    public static class ConnectionStrings
    {
        /// <summary>
        /// Name of the database connectionstring.
        /// </summary>
        public const string QuotableDb = nameof(QuotableDb);

        /// <summary>
        /// Name of the distributed cache connectionstring.
        /// </summary>
        public const string Valkey = nameof(Valkey);
    }

    /// <summary>
    /// Different key literals within the appsettings.
    /// </summary>
    public static class AppSettingKeys
    {
        /// <summary>
        /// gets audience for auth.
        /// </summary>
        public const string Audience = "Authentication:Audience";

        /// <summary>
        /// gets authority for auth.
        /// </summary>
        public const string Authority = "Authentication:Authority";

        /// <summary>
        /// gets metadata address for auth.
        /// </summary>
        public const string MetadataAddress = "Authentication:MetadataAddress";

        /// <summary>
        /// gets valid issuer for auth.
        /// </summary>
        public const string ValidIssuer = "Authentication:ValidIssuer";

        /// <summary>
        /// gets authorization url for auth.
        /// </summary>
        public const string AuthorizationUrl = "Keycloak:AuthorizationUrl";
    }

    /// <summary>
    /// Different endpoints and endpoint group names.
    /// </summary>
    public static class Endpoints
    {
        /// <summary>
        /// Name of the quotes endpoint group.
        /// </summary>
        public const string Quotes = nameof(Quotes);

        /// <summary>
        /// Name of the utilities endpoint group.
        /// </summary>
        public const string Utilities = nameof(Utilities);

        /// <summary>
        /// Names of the specific endpoints.
        /// </summary>
        public static class QuoteEndpoints
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
}
