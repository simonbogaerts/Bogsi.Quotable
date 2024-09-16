namespace Bogsi.Quotable.Web;

public static class Constants
{
    public const string QuotableDb = nameof(QuotableDb);
    public const string Keycloak = nameof(Keycloak);
    public const string Bearer = nameof(Bearer);

    public static class AppSettingKeys
    {
        public const string Audience = "Authentication:Audience";
        public const string Authority = "Authentication:Authority";
        public const string MetadataAddress = "Authentication:MetadataAddress";
        public const string ValidIssuer = "Authentication:ValidIssuer";
        public const string AuthorizationUrl = "Keycloak:AuthorizationUrl";
    }

    public static class Endpoints
    {
        public const string Quotes = nameof(Quotes);
        public const string Utilities = nameof(Utilities);

        public static class QuoteEndpoints
        {
            public const string GetQuotesEndpoint = nameof(GetQuotesEndpoint);
            public const string GetQuoteByIdEndpoint = nameof(GetQuoteByIdEndpoint);
            public const string CreateQuoteEndpoint = nameof(CreateQuoteEndpoint);
            public const string DeleteQuoteEndpoint = nameof(DeleteQuoteEndpoint);
        }
    }
}
