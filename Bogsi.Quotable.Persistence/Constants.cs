namespace Bogsi.Quotable.Persistence;

internal sealed record Constants
{
    public const string QuotableDb = nameof(QuotableDb);

    internal sealed record Schemas
    {
        public const string Quotable = nameof(Quotable);
    }

    internal sealed record Tables
    {
        public const string Quotes = nameof(Quotes);
    }

    internal sealed record Functions
    {
        public const string GetDate = "NOW()";
    }
}