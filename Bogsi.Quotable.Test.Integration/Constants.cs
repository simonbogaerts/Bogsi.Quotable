namespace Bogsi.Quotable.Test.Integration;

public sealed record Constants
{
    public static class Environments
    {
        public const string Testing = nameof(Testing);
    }

    public sealed record Database
    {
        public const string Image = "postgres:17rc1-alpine3.20";
        public const string Name = "quotable-db";
        public const string User = "quotable";
        public const string Password = "quotable123";
    }
}
