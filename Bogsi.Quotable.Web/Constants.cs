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
}
