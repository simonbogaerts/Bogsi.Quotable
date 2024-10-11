namespace Bogsi.Quotable.Test.Integration;

public sealed record Constants
{
    public sealed record Database
    {
        public const string Image = "postgres:17rc1-alpine3.20";
        public const string Name = "quotable-db";
        public const string User = "quotable";
        public const string Password = "quotable123";
    }

    public sealed record Valkey
    {
        public const string Image = "valkey/valkey:8.0.1-alpine";
        public const int Port = 6379;
    }
}
